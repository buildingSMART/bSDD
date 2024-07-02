In this tutorial, we explain how to manage bSDD content using [the Manage portal](https://manage.bsdd.buildingsmart.org/) once you have published it in bSDD. If you haven't, we recommend starting with the page about [Publishing data dictionaries in bSDD](https://technical.buildingsmart.org/services/bsdd/publishing/), which explains how to register an organisation and publish the first data dictionary.

### The lifecycle of a dictionary

When you publish a new dictionary version in the bSDD, it always initially has the `Preview` status. At this stage, you can **reupload** the content to modify it, **activate** that version, or permanently **delete** it.

<img src="https://raw.githubusercontent.com/buildingSMART/bSDD/master/Documentation/graphics/Content_lifecycle_workflow.jpg" alt="Lifecycle workflow" />

**⚠️ Once the content is activated, it will get an immutable URI, meaning the content will stay in bSDD permanently and can't be deleted.** It is still possible to change the status to `Inactive`, indicating it should no longer be used, but the page will still exist and show the content. Consider that before activating the version of a dictionary.

> Reminder: If you only want to experiment with the bSDD, we provide an option for a `TEST` upload. This is a safe option for beginners, as the content uploaded as a `TEST` cannot be activated and will automatically be removed after 2 months.

### Changing the dictionary status

The status can be changed through [the Manage portal](https://manage.bsdd.buildingsmart.org/) or via [the API](https://app.swaggerhub.com/apis/buildingSMART/Dictionaries/v1).

To do this, first, log in to [the Manage portal](https://manage.bsdd.buildingsmart.org/). Then, navigate to the (1) Dictionaries tab and select your organisation (2). If you belong to only one organisation, it will appear immediately on the list.

<img src="https://technical.buildingsmart.org/wp-content/uploads/2024/04/2024-04-05-22_17_58-bSDD.Management-1024x610.png" alt="" width="1267" height="550" />

### Publishing a new dictionary version

Similarly to publishing for the first time, in this view, you can also upload a new dictionary version by loading a properly structured JSON file (3) and clicking Upload (5). You have the option to first validate if the file is free of errors (4) or upload it for testing by selecting option (6).

As soon as you have at least one version of a dictionary uploaded, you will see a row in the table. There, you can see the name, version number and other properties of a version. By clicking `action`, you can download the JSON file to your computer (7), change the status to `Active` (8), or delete the version (9, only available if the status is `Preview`). You can delete the whole dictionary with all its versions with (10).

Buttons 11-12 allow you to turn the dictionary into a private one (11) and specify a list of users with access to such content (12). Private dictionaries are a paid option. You can read more about it here: [PRIVATE DICTIONARIES](https://github.com/buildingSMART/bSDD/blob/master/Documentation/bSDD%20private%20content.md).

> Note: All of the above can also be done through the API interface, meaning it is possible to achieve the same result with third-party software implementing bSDD API. 

## Read more 

* Publishing in bSDD
* Private dictionaries
* Guidelines
