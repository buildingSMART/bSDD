[![Official repository by buildingSMART International](https://img.shields.io/badge/buildingSMART-Official%20Repository-orange.svg)](https://www.buildingsmart.org/)

<img src="Documentation/graphics/bSDD_logo.png"
     alt="bSDD logo"
     style="width: 200px" />

**The buildingSMART Data Dictionary (bSDD)** is an online service that hosts classifications and their properties, allowed values, units and translations. The bSDD allows linking between all the content inside the database. It provides a standardized workflow to improve data quality and information consistency.

The most important part of the bSDD are the APIs, Application Programming Interfaces, accessible via the internet. Using those APIs other tools and systems can use the data stored in the bSDD.

More info on https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/

## Updates

> **2022-08 - Important notification:**
> 
> The bSDD is in the process of moving from identifiers (aka "namespace URI") starting with "http://identifier.buildingsmart.org" to "https://identifier.buildingsmart.org" ("http" to "https"). This is to ease the use of these identifiers as hyperlinks as well.
> 
> We will support retrieving data using the "http" identifiers for (at least) 6 months. But please do note that only "https" identifiers are returned by the bSDD API's. 
> Current status: **available in test environment**.

## Useful links

* FAQ (what does it cost? what is it? how does it relate to IFC? etc.): [buildingsmart.org/.../buildingsmart-data-dictionary/](https://www.buildingsmart.org/users/services/buildingsmart-data-dictionary/)
* Browse the documentation: [Documentation](Documentation)

### Using the bSDD

* Search in the bSDD: https://search.bsdd.buildingsmart.org/
* Upload data: https://manage.bsdd.buildingsmart.org/ 

We also provide a `Test` environment to play with, for those who want to experiment with bSDD first. We encourage to try `Test` before publishing in the actual bSDD. You can acces it via:  

* Search in the `Test` bSDD: https://search-test.bsdd.buildingsmart.org/
* Upload data to the `Test` database: https://manage-test.bsdd.buildingsmart.org/

> Note: 
> 
> The bSDD is meant to be API-first, so the Search Page doesn't display all the content of bSDD.
> 
> To be able to upload domains, create an account first and email to <bsdd_support@buildingsmart.org> to ask for access.
> 
> If you encounter a login issue on, e.g. some kind of looping, try clearing your browser cache or start an 'incognito' browser window.


### For developers 👩‍💻
* The API URL (not a readable website): https://api.bsdd.buildingsmart.org/ 
* API documentation on Swagger: https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1
* `Test` API documentation on Swagger: https://test.bsdd.buildingsmart.org/swagger/

### Instructions

* [How to upload your data into the bSDD?](/Documentation/bSDD%20import%20tutorial.md)
* [How to structure my data to import to bSDD?](/Documentation/bSDD%20JSON%20import%20model.md)

### Example use-cases
* A typical use-case for bSDD using SketchUp to create IFC data: [VIMEO video](https://vimeo.com/446417661/ff8b6605d3), and the [source code](https://github.com/DigiBase-VolkerWessels/SketchUp-bsDD-plugin)
* A typical use-case for bSDD using ACCA software: [YouTube video](https://www.youtube.com/watch?v=KTzJRJkisKk&ab_channel=ETIMInternational)
* GraphQL UI for getting data via GraphQL in the UI: [GraphQL UI](https://test.bsdd.buildingsmart.org/graphiql)
* Presentation about the bSDD (and relation to IFC): [Presentation](https://www.slideshare.net/berlotti/20200903-the-2020-buildingsmart-data-dictionary-prototype-bsdd)
* Video recording of a bSDD workshop, including Revit plugin demo: [Workshop video](https://app.box.com/s/lndnjrbx80n87eg1eq1zhhbqoz8hfmyz/file/720558204462 (start at 1.44.00))

## Contact us

Need help? Got suggestions? Send an email to <bsdd_support@buildingsmart.org>

The bSDD product manager: **Artur Tomczak** <artur.tomczak@buildingsmart.org>


