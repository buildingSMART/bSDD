# 🔐 Private data dictionaries

### What is a private dictionary in bSDD?
A private dictionary can only be accessed by designated users. To see the content, they need to be logged in and have permission from the dictionary owner. 

Private dictionaries are **a paid feature** of bSDD, priced individually. If you are interested in this feature, please contact bSDD_support@buildingsmart.org.  

### How to make a dictionary private?
Organization users with upload rights can make a dictionary Private by adding a line to their JSON files: `IsPrivate: true,` **in the first upload** of a dictionary. Subsequent uploads cannot change this setting.

Another option to make a dictionary private (or public again) is by manually changing the setting via the bSDD Management site:
→ "Dictionaries" → 'Make dictionary private' button for the desired dictionary. Such a dictionary needs to be uploaded as public first.

![image](https://github.com/buildingSMART/bSDD/assets/22922395/771ce6f5-45ab-4704-ad36-ba2670664654)


### Who can access my private dictionary?
1. people (e-mail addresses) listed as an "organization user" (no matter what rights, being in the list of organization users is enough to be able to access all private dictionaries of that organization).
2. access can be granted per dictionary (so **not** per dictionary version): either provide e-mail for giving access to one person or supply a hostname (e.g. "buildingsmart.org"), and all people with e-mails of that host can access the dictionary.

![image](https://github.com/buildingSMART/bSDD/assets/22922395/8792271b-724e-4993-b400-b61b2ee263c0)

![image](https://github.com/buildingSMART/bSDD/assets/22922395/517fc34e-020f-4e91-9b67-83a132a9e0e4)

### How can I access a private data dictionary?
You can only access private dictionary data when you're logged in as a user that is allowed to access it, and the application you are using supports it.

For example, to access the content via the Swagger page (Production: https://app.swaggerhub.com/apis-docs/buildingSMART/Dictionaries/v1 / Test: https://test.bsdd.buildingsmart.org/swagger), you should authorise and use the secured API's. Accessing the data via the unsecured APIs is possible, but Swagger does not support that (for those unsecured APIs, swagger does not send your 'token', so the server does not know who you are).

Currently, you cannot access private data via the search website (Production: https://search.bsdd.buildingsmart.org/ / Test: https://search-test.bsdd.buildingsmart.org).
