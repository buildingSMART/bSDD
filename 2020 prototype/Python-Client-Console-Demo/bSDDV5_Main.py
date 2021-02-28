
from bSDDV5_Classes import TClassification, TCountry, TDomain, TPostman
import requests
import msal

bsdd = TPostman() #used to handle the API calls

#---------------------------------------------------
#                        Authorize                 #
#---------------------------------------------------

def Login():
      
  bsdd.Authorize()
 
#---------------------------------------------------
#                        Get countries             #
#---------------------------------------------------

def Get_Countries():
      
  bsdd.get_Countries()

#---------------------------------------------------
#                        Get domains               #
#---------------------------------------------------

def Get_Domains():
      
  SaveResult = True
  NbDomains = bsdd.get_Domains(SaveResult)
  print ( str(NbDomains) + ' domains found')
  for item in bsdd.Domains:
      print("Domain: " + item.name + " --> " + item.namespaceUri)

#---------------------------------------------------
#                    Get all classes of a domain   #
#---------------------------------------------------

def Get_Classes_For_Domain():

  #Ask user the domain URI
  DomainURI = input("enter the domain URI : >") #we got them from the previous get domains call
  LanguageRequired = 'fr-FR'
  SaveResult = True
  Get_Details = True #Used to ask to get all classification details (including properties)
  NbClasses = bsdd.get_Domain_Classes(DomainURI, LanguageRequired, SaveResult, Get_Details)
  print ( str(NbClasses) + " found in the domain")

#-----------------------------------------------------------
#                    Get  classes linked to an IFC Entity  #
#-----------------------------------------------------------
    
def Get_Classes_Linked_To_IFC():
  #Ask user the domain URI
  DomainURI = input("enter the domain URI : >") # we got them from the previous get domains call
  IFCEntity = input("enter the IFC Entity name : >") # case sensitive
  LanguageRequired = 'EN'
  SaveResult = True
  Get_Details = False #Used to ask to get all classification details (including properties)
  NbClasses = bsdd.get_Linked_Classes(DomainURI, LanguageRequired, IFCEntity, SaveResult, Get_Details)
  print ( str(NbClasses) + " classification linked to " + IFCEntity)
      

#-----------------------------------------------------------
#                    Console APP example                   #
#-----------------------------------------------------------

Login()
Get_Countries()
Get_Domains()
#Get_Classes_For_Domain()
Get_Classes_Linked_To_IFC()


