### 1. Register your organisation

Each data dictionary in bSDD is published on behalf of a registered organisation. If this is the first time you are uploading, you need to register your organization in bSDD and connect the e-mail address you used to log in to that organization. To achieve this, please fill out the <a href="https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25">Organization registration form</a>.

As part of bSDD housekeeping, we manually review each request, which can take up to a few days. As soon as you've received a reply, you can proceed to the next step.

> Do you want to only experiment with bSDD without registering your organisation? We can add you to the DEMO organisation. For this and other requests, contact us: [CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h).

### 2. Prepare the content

The primary form of data upload to bSDD is a properly structured JSON file. In [the data model documentation](https://technical.buildingsmart.org/services/bsdd/data-structure/), we specify what such a file should contain and how to structure it.

You can manually create such a file by coping <a href="https://github.com/buildingSMART/bSDD/blob/master/Model/Import%20Model/bsdd-import-model.json">the JSON template</a>, or use <a href="https://github.com/buildingSMART/bSDD/tree/master/Model/Import%20Model/spreadsheet-import">the Excel template instructions</a>.

Alternatively, use one of <a href="https://technical.buildingsmart.org/resources/software-implementations/?filter_5=bSDD+submit%2Fmanage&amp;mode=any">the third-party tools to manage and upload data dictionaries in bSDD</a>.

### 3. Go to the Management portal of bSDD

Go to the Management portal: <a href="https://manage.bsdd.buildingsmart.org/">https://manage.bsdd.buildingsmart.org/</a>.

Alternatively, use one of <a href="https://technical.buildingsmart.org/resources/software-implementations/?filter_5=bSDD+submit%2Fmanage&amp;mode=any">the third-party tools to manage and upload data dictionaries in bSDD</a>, which integrates with <a href="https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1">the bSDD API</a>.

> Note: if the Management Portal shows an error at startup or you keep seeing the spinner icon, try pressing Ctrl-F5 to refresh the cookies. If that doesn't work, then try an "incognito" or "InPrivate" window of your browser and then navigate to the Management portal. If that still doesn't work, then contact us: [CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h).

### 4. Log in

If you do not have a bSDD buildingSMART account yet, choose "Sign up now"; otherwise, choose "Sign in".

### 5. Upload file

Go to the "Dictionaries" tab and select your organisation from the list.

Using the "Select file" button load your dictionary JSON file.

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/bSDD%20management%20portal.png" alt="bSDD manage" style="width: 800px" />

You have the option to first validate if the file is free of errors or upload it for testing by selecting option 'Test upload'. The test upload means the content will be automatically deleted from bSDD after 2 months and it will not be possible to set the status to 'Active' to prevent mistakes.

### 6. Press "Upload selected file"

Before each import, we recommend first using the option 'Validate only?' This will inform you of any errors or warnings without trying to import the file.

**Important Uploading a new file replaces the existing dictionary with the same version.**

Once ready, and if the platform returns no errors, click "Upload selected file."

Once the file has been imported, you will receive a more detailed import report by email. It might take up to 15 minutes. If the import routine spots any errors, you will see them listed in the email.

> Note: all of the steps explained above can also be automated using <a href="https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1">the bSDD API</a> integration.


## Read more

* How to manage content in bSDD: [https://technical.buildingsmart.org/services/bsdd/manage/](https://technical.buildingsmart.org/services/bsdd/manage/)
* Guidelines on data dictionary good practices: [https://technical.buildingsmart.org/services/bsdd/publishing/guidelines/](https://technical.buildingsmart.org/services/bsdd/publishing/guidelines/)
