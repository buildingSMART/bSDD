# How to import your bSDD model into bSDD?

If you have your model in the bsdd-import-model.json format available, you can upload it into the bSDD database yourself.

*Important* if you upload a file it will replace the whole domain! You cannot upload the classifications in parts, all classifications and properties of one domain must be in one file.

1. If it's the first time you are going to upload, you need to have your e-mail address, the one you used to log in, connected to your organization. To achieve this, send an e-mail with
- your name
- your e-mail address you used to log in the bSDD API
- the name of your organization
- a hyperlink to the website of your organization
- (optional) contact e-mail address in case users have questions about the domain you uploaded
to bsdd_support@buildingsmart.org.
As soon as you've got a reply, you can continue with the next step.

2. Go to [https://app.swaggerhub.com/apis/buildingSMART/Dictionaries-API/v1](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries-API/v1)
![bSDD API wagger](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_01_swagger.png)
Note: we're working on improving the user friendliness of the upload process.

3. Press "Authorize"
![Authorize for bSDD](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_02_authorize.png)

Fill in the Client ID: b222e220-1f71-4962-9184-05e0481a390d
Check the "read" scope checkmark and press Authorize.

4. If you do not have a bSDD buildingSMART account yet, choose "Sign up now", otherwise choose "Sign in"
![Signup/signin](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_03_signupsignin.png)

5. If all is well you should see the authorization confirmation:
![Authorization confirmation](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_04_authorization_confirmed.png)

Press "Close"

6. Click on the line with "/api/UploadImportFile"
![Upload Import File API](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_05_upload_1.png)

7. Press "Try it out"
![Try the upload](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_06_upload_2.png)

8. Enter the Organization Code of your organization (you should have received it by e-mail as a result of step 5), select your bSDD.json file and press Execute.
![Upload result](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_07_upload_result.png)

If there are any validation errors detected you will see them listed. You will receive a more detailed import report, one that might include warnings, by e-mail once the file has been imported.
