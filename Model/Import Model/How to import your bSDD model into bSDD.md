# How to import your bSDD model into bSDD?

If you have your model in the bsdd-import-model.json format available, you can upload it into the bSDD database yourself.

*Important* if you upload a file it will replace the whole domain! You cannot upload the classifications in parts, all classifications and properties of one domain must be in one file.

1. If it's the first time you are going to upload, you need to have your e-mail address, the one you used to log in, connected to your organization. To achieve this, send an e-mail with
- your name
- your e-mail address you use to log in the bSDD API
- the name of your organization
- a hyperlink to the website of your organization
- (optional) contact e-mail address in case users have questions about the domain you uploaded
to bsdd_support@buildingsmart.org.
As soon as you've got a reply, you can continue with the next step.

2. Go to [https://manage.bsdd.buildingsmart.org/](bSDD Management App)
or for test environment: [https://manage-test.bsdd.buildingsmart.org](bSDD Management App TEST)

3. Log in

4. If you do not have a bSDD buildingSMART account yet, choose "Sign up now", otherwise choose "Sign in"
![Signup/signin](https://raw.githubusercontent.com/buildingSMART/bSDD/master/Model/Import%20Model/doc_images/Screenshot_03_signupsignin.png)

5. If all is well you should see "Upload domain" menu item. Click this

6. Select the file you want to upload via button "Select file"

7. Optional: check the "Validate only?" checkbox if you only want to validate the file, not inserting the data

8. Press "Upload selected file"

If there are any validation errors detected you will see them listed. You will receive a more detailed import report, one that might include warnings, by e-mail once the file has been imported.
