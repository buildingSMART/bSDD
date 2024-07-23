[![Official repository by buildingSMART International](https://img.shields.io/badge/buildingSMART-Official%20Repository-orange.svg)](https://www.buildingsmart.org/)

<img src="Documentation/graphics/bSDD_logo.png"
     alt="bSDD logo"
     style="width: 200px" />

**The buildingSMART Data Dictionary (bSDD)** is an online service for hosting data dictionaries containing classifications, their properties, allowed values, units, translations, etc. It provides a standardized workflow to improve data quality and information consistency.

Read more at bSDD project page: https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/

### Overview

At the heart of bSDD is a canonical database, where all dictionaries can be related to each other. The main way to access the bSDD is through its [APIs (Application Programming Interfaces)](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1). This is how most BIM software and other apps can use the data stored in the bSDD. Apart from that, there is [the bSDD Search page](https://search.bsdd.buildingsmart.org/), where people can look up the content. Authors can publish content to bSDD through [the API](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1) or [the bSDD Manage portal](https://manage.bsdd.buildingsmart.org/). To upload, please register your organisation using [the organisation registration form](https://bsi-technicalservices.atlassian.net/servicedesk/customer/portal/3/group/4/create/25).

<img src="https://github.com/buildingSMART/bSDD/assets/22922395/0b581c14-fd16-402f-baa8-c55eac500eff"
     alt="bSDD diagram"
     style="width: 500px" />

### Quick links

* [bSDD project page](https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/)
* [bSDD Search page]()
* [bSDD Manage portal]()
* [bSDD API Swagger page]()
* [bSDD updates forum]()
* [bSDD data structure](/Documentation/bSDD%20JSON%20import%20model.md)
* [bSDD JSON template](/Model/Import%20Model/bsdd-import-model.json) / [bSDD Excel template](/Model/Import%20Model/spreadsheet-import)
* [Tools integrating bSDD](https://technical.buildingsmart.org/resources/software-implementations/?filter_5%5B%5D=bSDD%20read%20API&filter_5%5B%5D=bSDD%20submit%2Fmanage&filter_5%5B%5D=bSDD%20IFC%20export%20(including%20URIs)&filter_1=&gv_search=&mode=any). This is a self-managed list, so feel free to add missing ones.
* [How to upload your data into the bSDD?](/Documentation/bSDD%20import%20tutorial.md)

### For developers

📢 We inform about planned and recently implemented bSDD updates in this forum topic:
[bSDD Tech Updates](https://forums.buildingsmart.org/t/bsdd-tech-updates/4889).

* **API documentation** https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20API.md
* **API interactive documentation** on Swagger: https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1

We also provide a **TEST** environment where the latest features are rolled out first and tested. If you want to check it out, here are the equivalent pages (not to be used by end-users!):
* **TEST API documentation** on Swagger: https://test.bsdd.buildingsmart.org/swagger/
* **TEST GraphQL** environment UI: [GraphQL UI](https://test.bsdd.buildingsmart.org/graphiql)
and related Search/Manage pages:
* **TEST Search** page: https://search-test.bsdd.buildingsmart.org/
* **TEST Manage** portal: https://manage-test.bsdd.buildingsmart.org/

## Contact us

Need help? Got suggestions? Contact us: [CONTACT FORM](https://share.hsforms.com/1RtgbtGyIQpCd7Cdwt2l67A2wx5h).

bSDD is one of our [Strategic Projects](https://www.buildingsmart.org/about/strategic-projects/), meaning buildingSMART International is calling on industry sponsorship to help fund the delivery of bSDD improvements.
