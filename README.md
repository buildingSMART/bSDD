[![Official repository by buildingSMART International](https://img.shields.io/badge/buildingSMART-Official%20Repository-orange.svg)](https://www.buildingsmart.org/)


# bSDD buildingSMART Data Dictionary (bSDD)
The buildingSMART Data Dictionary (bSDD) is an online service that hosts classifications and their properties, allowed values, units and translations. The bSDD allows linking between all the content inside the database. It provides a standardized workflow to improve data quality and information consistency.

The most important part of the bSDD are the APIs, Application Programming Interfaces, accessible via the internet. Using those APIs other tools and systems can use the data stored in the bSDD.

More info on https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/


**2022-08 - Important notification:**

The bSDD is in the process of moving from identifiers (aka "namespace URI") starting with "http://identifier.buildingsmart.org" to "https://identifier.buildingsmart.org" ("http" to "https"). This is to ease the use of these identifiers as hyperlinks as well.

We will support retrieving data using the "http" identifiers for (at least) 6 months. But please do note that only "https" identifiers are returned by the bSDD API's.

Current status: available in test environment.

**End of notification**

## Very useful links

 * FAQ (what does it cost? what is it? how does it relate to IFC? etc..): [bSDD](https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/)

## Test the bSDD
* Website to manually explore the bSDD APIs in the test environment: https://test.bsdd.buildingsmart.org/swagger/
* Search in the bSDD test database: https://search-test.bsdd.buildingsmart.org/
* Upload your data in the test database: https://manage-test.bsdd.buildingsmart.org/
* How to upload your data into the bSDD: [empty JSON model](https://github.com/buildingSMART/bSDD/blob/master/Model/Import%20Model/bsdd-import-model.json) and [import model description](https://github.com/buildingSMART/bSDD/blob/master/Model/Import%20Model/bSDD%20JSON%20import%20model.md).
Data owners are encouraged to put their data in the test environment first, and communicate to bsdd_support@buildingsmart.org when they want to push it to the production environment.

** If you encounter a login issue on https://maanage-test.bsdd.buildingsmart.org or https://manage.bsdd.buildingsmart.org, e.g. some kind of looping, try clearing your browser cache or start an 'incognito' browser window. **

## Use the bSDD
 * Upload data: https://manage.bsdd.buildingsmart.org/
 * The API URL: https://api.bsdd.buildingsmart.org/
 * API documentation (aka swagger): https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1
 * Search in the bSDD: https://search.bsdd.buildingsmart.org/

## Nice to know
* A typical use-case for bSDD using SketchUp to create IFC data: [VIMEO video](https://vimeo.com/446417661/ff8b6605d3) [source code](https://github.com/DigiBase-VolkerWessels/SketchUp-bsDD-plugin)
* A typical use-case for bSDD using ACCA software: [YouTube video](https://www.youtube.com/watch?v=KTzJRJkisKk&ab_channel=ETIMInternational)
* GraphQL UI for getting data via GraphQL in the UI: [GraphQL UI](https://test.bsdd.buildingsmart.org/graphiql)
* Presentation about the bSDD (and relation to IFC): [Presentation](https://www.slideshare.net/berlotti/20200903-the-2020-buildingsmart-data-dictionary-prototype-bsdd)
* Video recording of a bSDD workshop, including Revit plugin demo: [Workshop video](https://app.box.com/s/lndnjrbx80n87eg1eq1zhhbqoz8hfmyz/file/720558204462 (start at 1.44.00))
* Get help: send an email to bsdd_support@buildingsmart.org


