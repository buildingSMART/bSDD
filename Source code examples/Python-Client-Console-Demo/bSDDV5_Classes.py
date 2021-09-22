import csv
import requests
import requests.auth

#API_Endpoint = "https://bsdd-prototype.azure-api.net/api/" 
API_EndPoint = 'https://test.bsdd.buildingsmart.org/api/';

# API ressources
Resource_Domains = "Domain/v2"
Resource_Classification = "Classification/v2"
Resource_Country = "Country/v1"
Resource_Search_Open = 'SearchListOpen/v2' #Unsecured
Resource_Search_Secured = 'SearchList/v2' #secured


class TObject():
    name = ''
    namespaceUri = ''

    #----------------------------------------------------------------------------------------------------
    # Read a JSON value 
    def ReadVal(self, _JObj, _key):
        
        res = ''
        
        if _key in _JObj:
          try :
            res = _JObj[_key]
          except: 
            res ='error'
        
        return res
    #----------------------------------------------------------------------------------------------------
        
#----------------------#        
#  Country             #   
#----------------------#        
class TCountry(TObject):
    code = ''
    
    def FillValuesFromJSON(self, _content):
        self.name = self.ReadVal(_content, 'name')
        self.code = self.ReadVal(_content, 'code')

#----------------------#        
#  Classification      #   
#----------------------#        
class TClassification(TObject):    
    definition = ''
    IFCLinks = []
    Properties = []

    # Read the values from the JSON response    
    def FillValuesFromJSON(self, _content):
        self.name = self.ReadVal(_content, 'name')
        self.namespaceUri = self.ReadVal(_content, 'namespaceUri') 
        self.definition = self.ReadVal(_content, 'definition') 

    # Ask the API for the classification details
    def Load_Details(self, _content):
          if "relatedIfcEntityNames" in _content:
                for item in _content["relatedIfcEntityNames"]:
                      self.IFCLinks.append(item)
                      
          # Properties attached to the classification
          if "classificationProperties" in _content:
                    for item in _content["classificationProperties"]:                                  
                      NewProperty = TProperty()
                      NewProperty.FillValuesFromJSON(item)                      
                      self.Properties.append(NewProperty)

    # Save properties list  values  of the class into a csv file
    def SaveToCSV(self, _Name):
        with open(_Name + '_Properties.csv' , 'w+') as csvfile:
            MyFields = ['Name' , 'Domain', 'URI', 'Definition', 'dataType']
            writer = csv.DictWriter(csvfile, delimiter=';' , fieldnames=MyFields)
            writer.writeheader()
            for item in self.Properties:
                writer.writerow({'Name' : item.name, 'Domain': item.domain , 'URI' : item.namespaceUri, 'Definition' : item.definition, 'dataType' : item.dataType})

#----------------------#        
#  Property            #   
#----------------------#        
class TProperty(TObject):    
    definition = ""
    domain = ""
    dataType = ""

    # Read the values from the JSON response    
    def FillValuesFromJSON(self, _content):
        self.name = self.ReadVal(_content, 'name')
        self.domain = self.ReadVal(_content, 'propertyDomainName') 
        self.namespaceUri = self.ReadVal(_content, 'propertyNamespaceUri') 
        self.definition = self.ReadVal(_content, 'description') 
        self.dataType = self.ReadVal(_content, "dataType")
     
#----------------------#        
#  Domain              #   
#----------------------#        
class TDomain(TObject):
    version = ''
    organizationNameOwner = ''
    defaultLanguageCode = ''
    license = ''
    licenseUrl = ''
    qualityAssuranceProcedure = ''
    qualityAssuranceProcedureUrl = ''
    Classes = []

    # Read the values from the JSON response
    def FillValuesFromJSON(self, _content):
      self.namespaceUri = self.ReadVal(_content, 'namespaceUri')
      self.name = self.ReadVal(_content, 'name')
      self.version = self.ReadVal(_content, 'version')
      self.organizationNameOwner = self.ReadVal(_content, 'organizationNameOwner')
      self.defaultLanguageCode = self.ReadVal(_content, 'defaultLanguageCode')
      self.license = self.ReadVal(_content, 'license')
      self.licenseUrl = self.ReadVal(_content, 'licenseUrl')
      self.qualityAssuranceProcedure = self.ReadVal(_content, 'qualityAssuranceProcedure')
      self.qualityAssuranceProcedureUrl = self.ReadVal(_content, 'qualityAssuranceProcedureUrl')

    # Save classifications list  values  of the domain into a csv file
    def SaveToCSV(self):
        with open(self.name + '_Classes.csv' , 'w+') as csvfile:
            MyFields = ['Name' , 'URI', 'Definition']
            writer = csv.DictWriter(csvfile, delimiter=';' , fieldnames=MyFields)
            writer.writeheader()
            for item in self.Classes:
                writer.writerow({'Name' : item.name, 'URI' : item.namespaceUri, 'Definition' : item.definition})
        
#------------------------------------------------------------------------#
#        This class contains                                             # 
#           - authorization                                              #
#           - Header formatting of a request (with & without token)      #
#           - Send of a Get request for a specific ressource             #
#           - Calls to the API Ressources                                #
#------------------------------------------------------------------------#

class TPostman():
    
    Domains = [] # Used to store domains received from an API call
    Countries = [] # used to store the list of countries received from an API call
    Token = None # used to store the token received from an authorization call
    #----------------------------------------------------------------------------------------------------

    #----------------------------------------------------------------------------------------------------
    # get an authorization token - will open a web browser for the credentials
    #----------------------------------------------------------------------------------------------------

    def Authorize(self):

      bSDD_ClientID = '4aba821f-d4ff-498b-a462-c2837dbbba70'                             
      bSDD_Scope = "https://buildingsmartservices.onmicrosoft.com/api/read"
      
      from msal import PublicClientApplication

      bSDD_authority = 'https://buildingsmartservices.b2clogin.com/tfp/buildingsmartservices.onmicrosoft.com/b2c_1_signupsignin'
      
      app = PublicClientApplication( bSDD_ClientID, authority = bSDD_authority) #need localhost redirection on azure portal for the client ID
      flow = app.initiate_auth_code_flow(scopes=[bSDD_Scope])
      self.Token = app.acquire_token_interactive(scopes=[bSDD_Scope])

      print('logged in')
     
    #----------------------------------------------------------------------------------------------------
    # Format the header of a query 
    #----------------------------------------------------------------------------------------------------

    def setHeader(self):                 

        if self.Token:
         myheader = {
          'content-type': 'application/json',
          'Accept' :'application/json', # We want JSON
          'Authorization' : 'Bearer ' + (self.Token['access_token']) #if logged in, here is the received token to send for secured API call
         }
        else : 
         myheader = {
          'content-type': 'application/json',
          'Accept' :'application/json', 
         }

        return myheader    

    #----------------------------------------------------------------------------------------------------
    # Send a GET API Call
    #----------------------------------------------------------------------------------------------------

    def get(self, _resource, _params):
      
      #print (_params)#Uncomment to check the parameters sent  
      mResponse = requests.get(API_EndPoint + _resource, headers=self.setHeader(), params=_params)
      #print (mResponse.url) #uncomment to see the URL sent
      #print(mResponse.text) #uncomment to see the result of the call in the console
      #sometimes it is needed to be able to access the header of the response
      self.header = mResponse.headers
      
      return mResponse.json()

    #----------------------------------------------------------------------------------------------------
    # Get available countries
    #----------------------------------------------------------------------------------------------------

    def get_Countries(self):

      Response = self.get(Resource_Country, "")
      #Browse the results
      NbRes = 0

      for item in Response:
        Country = TCountry()
        Country.FillValuesFromJSON(item)
        self.Countries.append(Country)

    #----------------------------------------------------------------------------------------------------
    # Get available domains
    #----------------------------------------------------------------------------------------------------
    
    def get_Domains(self, _SaveResult):
      
      Response = self.get(Resource_Domains, "")
      #Browse the results
      NbRes = 0
      
      for item in Response:
        Domain = TDomain()
        Domain.FillValuesFromJSON(item)
        self.Domains.append(Domain)
        NbRes = NbRes + 1
      
      if _SaveResult:
        #Save domain list to a csv file
        self.Save_Domains_To_CSV()

      return NbRes

    #----------------------------------------------------------------------------------------------------
    # Get Classes of domains
    #----------------------------------------------------------------------------------------------------

    def get_Domain_Classes(self, _DomainURI, _LanguageCode, _SaveResult, _Get_Details):
      
      # params of the request
      payload = dict()
      payload["DomainNamespaceUri"] = _DomainURI
      #Language code for the request ; EN for Internation English
      payload["LanguageCode"] = _LanguageCode
      
      Response = self.get(Resource_Search_Secured, payload)

      NbRes = Response["numberOfClassificationsFound"]
      
      for item in Response['domains']: #in this case we should have just 1 !
        ReadDomain = self.GetDomainFromURI(item['namespaceUri']) 
        for item2 in item["classifications"]:
          NewClass = TClassification()
          NewClass.FillValuesFromJSON(item2)
          ReadDomain.Classes.append(NewClass)

          #If details are required, a request is launched for each class
          if _Get_Details:
                payloadClass = dict()
                payloadClass["namespaceUri"] = NewClass.namespaceUri
                payloadClass["languageCode"] = _LanguageCode
                payloadClass["includeChildClassificationReferences"] = False #we don't ask for the hierarchy we just want properties
                mResponse = self.get(Resource_Classification, payloadClass)
                NewClass.Load_Details(mResponse)

        if _SaveResult:
          #Save the classes informations to a csv    
          ReadDomain.SaveToCSV();

      return NbRes

    #----------------------------------------------------------------------------------------------------
    # Retrieve the properties of a classification
    #----------------------------------------------------------------------------------------------------

    def Get_Classification_Properties(self, _ClassificationURI, _LanguageCode, _SaveResult, _ClassificationName = None): #Classification name, optional, just to nicely name the excel export
       payloadClass = dict()
       payloadClass["namespaceUri"] = _ClassificationURI
       payloadClass["languageCode"] = _LanguageCode
       payloadClass["includeChildClassificationReferences"] = False #we don't ask for the hierarchy we just want properties
       mResponse = self.get(Resource_Classification, payloadClass)

       mClassification = TClassification()

       NbRes = 0

       if "relatedIfcEntityNames" in mResponse:
              for item in mResponse["relatedIfcEntityNames"]:
                  mClassification.IFCLinks.append(item)
                      
        # Get the properties attached to the classification
       if "classificationProperties" in mResponse:
            for item in mResponse["classificationProperties"]:                                  
              mProperty = TProperty()
              mProperty.FillValuesFromJSON(item)                      
              mClassification.Properties.append(mProperty)
              NbRes += 1

       #Save the properties informations to a csv    
       if _SaveResult:          
          mClassification.SaveToCSV(_ClassificationName)       

       return NbRes
          

    #----------------------------------------------------------------------------------------------------
    # Retrieve a the classes of a domain linked to an IFC Entity 
    #----------------------------------------------------------------------------------------------------

    def get_Linked_Classes(self, _DomainURI, _LanguageCode, _IFCEntity, _SaveResult, _Get_Details):
      # params of the request
      payload = dict()
      payload["DomainNamespaceUri"] = _DomainURI
      #Language code for the request ; EN for Internation English
      payload["LanguageCode"] = _LanguageCode
      payload["RelatedIfcEntity"] = _IFCEntity
      
      Response = self.get(Resource_Search_Open, payload)
      
      NbRes = Response["numberOfClassificationsFound"]

      for item in Response['domains']: #in this case we should have just 1 !
        ReadDomain = self.GetDomainFromURI(item['namespaceUri']) 
        for item2 in item["classifications"]:
          NewClass = TClassification()
          NewClass.FillValuesFromJSON(item2)
          ReadDomain.Classes.append(NewClass)

          #If details are required, a request is launched for each one
          if _Get_Details:
                payloadClass = dict()
                payloadClass["namespaceUri"] = NewClass.namespaceUri
                payloadClass["languageCode"] = _LanguageCode
                payloadClass["includeChildClassificationReferences"] = False #we don't ask for the hierarchy we just want properties
                mResponse = self.get(Resource_Classification, payloadClass)
                NewClass.Load_Details(mResponse)

        if _SaveResult:
          #Save the classes informations to a csv    
          ReadDomain.SaveToCSV();

      return NbRes

    #----------------------------------------------------------------------------------------------------
    # Retrieve a domain from its URI, in the list of domains got from the API ressource "Domain"
    #----------------------------------------------------------------------------------------------------

    def GetDomainFromURI(self, _URI):

      i = 0;

      for item in self.Domains:
            if item.namespaceUri == _URI:
                  return item
                  break

    #----------------------------------------------------------------------------------------------------
    # Save domains list into a csv file
    #----------------------------------------------------------------------------------------------------

    def Save_Domains_To_CSV(self):

      with  open('bSDD_Domains.csv' , 'w+') as csvfile:

        MyFields = ['Name' , 'URI']
        writer = csv.DictWriter(csvfile, delimiter=';' , fieldnames=MyFields)
        writer.writeheader()
        for item in self.Domains:
          writer.writerow({'Name' : item.name, 'URI' : item.namespaceUri})
