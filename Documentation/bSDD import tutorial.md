In this tutorial, we explain how to publish and manage bSDD content using [the bSDD Manage portal](https://manage.bsdd.buildingsmart.org/).

## Publishing the first dictionary

### 1. Register your organisation

Each data dictionary in bSDD is published on behalf of a registered organisation. If this is the first time you are uploading, you need to register your organization in bSDD and connect the e-mail address you used to log in to that organization. To achieve this, please fill out the <a href="https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25">Organization registration form</a>.

As part of bSDD housekeeping, we manually review each request, which can take up to a few days. As soon as you've received a reply, you can proceed to the next step.

> Do you want to only experiment with bSDD without registering your organisation? We can add you to the DEMO organisation. For this and other requests, contact us: [CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h).

### 2. Prepare the content

The primary form of data upload to bSDD is a properly structured JSON file. In [the data model documentation](https://technical.buildingsmart.org/services/bsdd/data-structure/), we specify what such a file should contain and how to structure it.

You can manually create such a file by coping <a href="https://github.com/buildingSMART/bSDD/blob/master/Model/Import%20Model/bsdd-import-model.json">the JSON template</a>, or use <a href="https://github.com/buildingSMART/bSDD/tree/master/Model/Import%20Model/spreadsheet-import">the Excel template instructions</a>. Alternatively, use one of <a href="https://technical.buildingsmart.org/resources/software-implementations/?filter_5=bSDD+submit%2Fmanage&amp;mode=any">the third-party tools to manage and upload data dictionaries in bSDD</a>.

#### General guidelines

- Fill in all the required attributes.
- You cannot upload the content in parts. All classes and properties of one dictionary must be in one file.
- Manage and publish translations in a separate file.
- Make specific dictionaries; don't try to put too much data into one dictionary.
- Link new classes to IFC entities for increased usability. This way, the software knows how to represent the class in IFC.
- Add existing properties to your content instead of replicating them.
- When assigning property to a class, give it a 'property set' name to know how to structure it in a model (avoid using the 'Pset_' prefix. This is restricted to IFC only)
- Naming conventions and guidelines - the dictionary name needs to be unique. Avoid the use of a name that is too generic. Avoid names that conflict with other dictionaries. For example, do not create classes with an 'Ifc' prefix. Avoid replicating content from other dictionaries. Some licenses do not allow redistribution or modifications. It's a good practice to reuse content by linking it to your dictionary. For example, you can add properties from other dictionaries to your class. 
- Dictionary code - the dictionary code needs to be unique in the bSDD; choose one that is recognizable with the dictionary name. The dictionary code is used to generate the URIs of all the resources, so it should be short and preferably without spaces. 

**Read more** about good practices for creating data dictionaries: [https://technical.buildingsmart.org/services/bsdd/guidelines/](https://technical.buildingsmart.org/services/bsdd/guidelines/)

### 3. Upload

Go to [the bSDD Manage portal](https://manage.bsdd.buildingsmart.org/). If you do not have a bSDD buildingSMART account yet, choose "Sign up now"; otherwise, choose "Sign in".

> Alternatively, use one of <a href="https://technical.buildingsmart.org/resources/software-implementations/?filter_5=bSDD+submit%2Fmanage&amp;mode=any">the third-party tools to manage and upload data dictionaries in bSDD</a>, which integrates with <a href="https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1">the bSDD API</a>.

> Note: if the bSDD Manage portal shows an error at startup or you keep seeing the spinner icon, try pressing Ctrl-F5 to refresh the cookies. If that doesn't work, then try an "incognito" or "InPrivate" window of your browser and then navigate to the bSDD Manage portal. If that still doesn't work, then contact us: [CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h).

Go to the Dictionaries tab and select your organisation. If you belong to only one organisation, it will appear immediately on the list.

Using the "Select file" button load your dictionary JSON file.

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/bSDD%20management%20portal.png" alt="bSDD manage" style="width: 800px" />

You have the option to first validate if the file is free of errors or upload it for testing by selecting option 'Test upload'. The test upload means the content will be automatically deleted from bSDD after 2 months and it will not be possible to set the status to 'Active' to prevent mistakes.

> Note: If you only want to experiment with the bSDD, we provide an option for a `TEST` upload. This is a safe option for beginners, as the content uploaded as a `TEST` cannot be activated and will automatically be removed after 2 months.

Press "Upload selected file"

Before each import, we recommend first using the option 'Validate only?' This will inform you of any errors or warnings without trying to import the file.

**Important** Uploading a new file with the same version number as already existing will replace the content (only if status is `Preview`, as all other content is immutable - [read more below](#the-lifecycle-of-a-dictionary)).

Once ready, and if the platform returns no errors, click "Upload selected file."

Once the file has been imported, you will receive a more detailed import report by email. It might take up to 15 minutes. If the import routine spots any errors, you will see them listed in the email.

**Important** Uploading will make the content publicly available. Do not publish anything that you don't want to share with the general public, or you don't have sufficient permission. 

> Note: it is possible to restrict the visibility of a dictionary only to certain users. However, this is a paid feature of bSDD. Read more about [Private dictionaries](https://technical.buildingsmart.org/services/bsdd/private-dictionaries/).

> Note: all of the steps explained above can also be automated using <a href="https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1">the bSDD API</a> integration.

## The lifecycle of a dictionary

When you publish a new dictionary version in the bSDD, it always initially has the `Preview` status. At this stage, you can **reupload** the content to modify it, **activate** that version, or permanently **delete** it.

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/Content_lifecycle_workflow.jpg" alt="Lifecycle workflow" />

**⚠️ Once the content is activated, it will get an immutable URI, meaning the content will stay in bSDD permanently and can't be deleted.** It is still possible to change the status to `Inactive`, indicating it should no longer be used, but the page will still exist and show the content. Consider that before activating the version of a dictionary.

## Publishing a new dictionary version

Similarly to publishing for the first time, you can also upload a new dictionary version by loading a properly structured JSON file and clicking Upload.

## Changing the dictionary status

As soon as you have at least one version of a dictionary uploaded, you will see a row in the table with the name, version number and other properties of each version. By clicking `action`, you can **download** the JSON file to your computer, **change the status** to `Active`, or **delete** the version (both options are only available if the status is `Preview`).

If this option is enabled, the user can change the dictionary into a private one and specify a list of users with access to such content. Private dictionaries are a paid option. You can read more about it here: [Private dictionaries](https://technical.buildingsmart.org/services/bsdd/private-dictionaries/).

> Note: All of the above can also be done through the API interface, meaning it is possible to achieve the same result with third-party software implementing bSDD API. 
