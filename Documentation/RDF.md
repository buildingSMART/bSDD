# RDF

Resource Description Framework (RDF) is a standard model for data interchange on the Web. Read more: https://www.w3.org/RDF/

The bSDD has a feature that returns data in RDF format, but it's currently in PREVIEW status.

The following API's support returning data in RDF:

- /api/Classification/v3

You can request output in RDF-xml format by adding an HTTP header with key "Accept" and value "application/rdf+xml".

You can request output in turtle format by adding an HTTP header with key "Accept" and value "text/turtle" of "application/x-turtle".

<img src="https://github.com/buildingSMART/bSDD/blob/documentation/Documentation/graphics/HowToGetOutputInTurtleFormat.PNG" alt="How to get output in turtle format"/>
