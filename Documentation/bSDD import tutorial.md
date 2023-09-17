# The lifecycle of the bSDD dictionary version

<img src="/Documentation/graphics/Content_lifecycle_workflow.jpg" alt="Lifecycle workflow" style="width: 900px">

When you publish a new version of a dictionary in bSDD, it has the `Preview` status. At this stage, you can *reupload* the content to modify it, *activate* the version, or permanently *delete* it. The status can be changed through the [Management Portal](https://manage.bsdd.buildingsmart.org/) or via [the API](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1).

⚠️ Once the content is activated, it will get an immutable URI, meaning the URL and content will stay in bSDD forever, and can't be deleted. Changing the status to "Inactive" is possible, but the page will still show the content to support use in contractual agreements. Consider that before activating the version of a dictionary.


# How to import your bSDD model into bSDD?

If you have your model in the bsdd-import-model.json format available, you can upload it into the bSDD database yourself.

**Important** if you upload a file it will replace the whole dictionary! You cannot upload the classes in parts, all classes and properties of one dictionary must be in one file.

1. If it's the first time you are going to upload, you need to have your organization registered in bSDD, and the e-mail address you used to log in connected to that organization. To achieve this, please fill the form: [Organization registration form](https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25). In case the organization exists, but you can't access the content, contact us at: <a href="mailto:bsdd_support@buildingsmart.org">bsdd_support@buildingsmart.org</a>

As soon as you've got a reply, you can continue with the next step.

2. Go to:
-  [https://manage-test.bsdd.buildingsmart.org](https://manage-test.bsdd.buildingsmart.org) for the TEST environment*
-  [https://manage.bsdd.buildingsmart.org/](https://manage.bsdd.buildingsmart.org/) for the PRODUCTION environment

*We recommend everyone to first test the bSDD using the TEST environment and when ready, start using the PRODUCTION one.

3. Log in

> If you do not have a bSDD buildingSMART account yet, choose "Sign up now", otherwise choose "Sign in"
> 
> <img src="/Documentation/graphics/Screenshot_03_signupsignin.png" alt="Signup/signin" style="width: 400px">

4. If all is well you should see "Upload dictionary" menu item. Click on it.

5. Select the JSON file you want to upload via button "Select file".
> If you don't know how to create the proper JSON file, read that section: [bSDD JSON import model](/Documentation/bSDD%20JSON%20import%20model.md).

6. Optional: check the "Validate only?" checkbox if you only want to validate the file, not inserting the data

7. Press "Upload selected file"

> If there are any validation errors detected you will see them listed. You will receive a more detailed import report, one that might include warnings, by e-mail once the file has been imported.

# General guidelines

- Manage and publish translations in a seperate files.
- Make specific dictionaries, don't try to put too much data into one dictionary.
- Link to IFC entities for increased usability.
- Add IFC properties to your content instead of replicating your own properties.
- Make sure you use the correct encoding of characters. 

# Naming conventions and guidelines
Avoid names that conflict with other dictionaries. For example, do not create classes with an 'Ifc'-prefix. Do not replicate content from other dictionaries, but link to it. 

## Domain name
The dictionary name needs to be unique. Avoid the use of a name that is generic; choose a dictionary name that is specific. Global names like 'building products' are not allowed. Add your region or applition (use case application, not software) to the name of the dictionary to make it specific.

## Domain code
The dictionary code needs to be unique in the bSDD; choose one that is recognizable with the dictionary name.
The dictionary code is used in the URI of all the data, so choose carefully. 

## Organisation name
The organisation name (and URI code) will be assigned by the bSDD team, but suggestions can be made.

## Property set names
Avoid the use of 'Pset_' prefix. This is restricted for IFC only.
Avoid replicating the name of the property or class.
