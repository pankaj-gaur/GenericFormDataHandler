GenericFormDataHandler
======================

Generic solutions to manage the submitted data of Website Form post submission

Accept a JSON in below format:
{
    "FormID": "MyForm",
    "ProjectID": "MyWebsite.com",
    "FormData": "Content In Any Form (JSON or XML)",
    "UserIP": "",
    "UserLocation": "",
    "Format": "",
    "SubmitDate": ""
}
And insert it into a Database Table.

While Submitting only FormID, ProjectID and FormData fields are required in the above JSON.

Optionally accept FormID, Project ID, Submit Date and Format in the above JSON and return Submitted Form Data in JSON or XML format.
The filtering happens on one or more of the above field. If none of these field are supplied, all submitted form data for all forms and project will be returned. If format is not given, by default the data will be returned in JSON format.

How to Test:
•	Create a Database Instance with Tables and Stored Procedure as per the diagram and SQL available in the "Database" folder
•	Open the Solution and update the connection string of the DB in the web.config file
•	Use Fidler to Post some data – Couple of sample JSON are in the "SampleJSON" folder
•	Verify in the DB the data has been inserted
•	On your browser hit – http://<locahost:port>/api/formsdata and you will see all the data available in the DB
