# How to upload data dictionary to bSDD

**1. Prepare the content**

The primary form of data upload to bSDD is a properly structured JSON file. In [the data model documentation](https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20JSON%20import%20model.md), we specify what such a file should contain and how to structure it.

You can manually create such a file by coping [the JSON template](https://github.com/buildingSMART/bSDD/blob/master/Model/Import%20Model/bsdd-import-model.json), or use [the Excel template instructions](https://github.com/buildingSMART/bSDD/tree/master/Model/Import%20Model/spreadsheet-import).

Alternatively, use one of [the third-party tools to manage and upload data dictionaries in bSDD](https://technical.buildingsmart.org/resources/software-implementations/?filter_5=bSDD+submit%2Fmanage&mode=any).

**2. Register your organisation**

Each data dictionary in bSDD is published on behalf of a registered organisation. If this is the first time you are uploading, you need to register your organization in bSDD and connect the e-mail address you used to log in to that organization. To achieve this, please fill out the [Organization registration form](https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25). 

As part of bSDD housekeeping, we manually review each request. For that reason, it can take up to a few days. As soon as you've received a reply, you can proceed to the next step.

Do you want to only experiment with bSDD without registering your organisation? We can add you to the DEMO organisation. For this and other requests, contact us at: <a href="mailto:bsdd_support@buildingsmart.org">bsdd_support@buildingsmart.org</a>

**3. Go to the Management portal of bSDD**

Go to the Management portal: [https://manage.bsdd.buildingsmart.org/](https://manage.bsdd.buildingsmart.org/).

Alternatively, use one of [the third-party tools to manage and upload data dictionaries in bSDD](https://technical.buildingsmart.org/resources/software-implementations/?filter_5=bSDD+submit%2Fmanage&mode=any).

Note: if the Management Portal shows an error at startup or you keep seeing the spinner icon, try pressing Ctrl-F5 to refresh the cookies. If that doesn't work, then try an "incognito" or "InPrivate" window of your browser and then navigate to the Management portal. If that still doesn't work, then contact us: <a href="mailto:bsdd_support@buildingsmart.org">bsdd_support@buildingsmart.org</a>.

**4. Log in**

If you do not have a bSDD buildingSMART account yet, choose "Sign up now", otherwise choose "Sign in".

**5. Upload file**

Go to the "Dictionaries" tab and select your organisation from the list. 

Using the "Select file" button load your dictionary JSON file. 

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/bSDD%20management%20portal.png" alt="bSDD manage" style="width: 800px"/>

**6. Press "Upload selected file"**

Before each import, we recommend first using the option 'Validate only?' This will inform you of any errors or warnings without trying to import the file.

**Important** Uploading a new file replaces the existing dictionary with the same version.

Once ready, and if the platform returns no errors, click "Upload selected file." 

Once the file has been imported, you will receive a more detailed import report by email. It might take up to 15 minutes. In case the import routine spots any errors, you will see them listed in the email.

# The lifecycle of the bSDD dictionary version

When you publish a new dictionary version in bSDD, it initially always has the `Preview` status. At this stage, you can *reupload* the content to modify it, *activate* that version, or permanently *delete* it. The status can be changed through the [Management Portal](https://manage.bsdd.buildingsmart.org/) or via [the API](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1).

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/Content_lifecycle_workflow.jpg" alt="Lifecycle workflow" style="width: 900px">

⚠️ Once the content is activated, it will get an immutable URI, meaning the URL and content will stay in bSDD forever and can't be deleted. Changing the status to "Inactive" is possible, but the page will still show the content to support use in contractual agreements. Consider that before activating the version of a dictionary.

# General guidelines

- Fill in all the required attributes.
- You cannot upload the content in parts. All classes and properties of one dictionary must be in one file.
- Manage and publish translations in a separate file.
- Make specific dictionaries; don't try to put too much data into one dictionary.
- Link to IFC entities for increased usability.
- Add existing properties to your content instead of replicating them. 

### Naming conventions and guidelines
The dictionary name needs to be unique. Avoid the use of a name that is too generic. Avoid names that conflict with other dictionaries. For example, do not create classes with an 'Ifc' prefix. Avoid replicating content from other dictionaries. Some licenses do not allow redistribution or modifications. It's a good practice to reuse content by linking it to your dictionary. For example, you can add properties from other dictionaries to your class. 

### Dictionary code
The dictionary code needs to be unique in the bSDD; choose one that is recognizable with the dictionary name. The dictionary code is used to generate the URIs of all the resources, so it should be short and preferably without spaces. 

### Property set names
Avoid using the 'Pset_' prefix. This is restricted to IFC only. Do not replicate the property or class name.
