***First Impression***<br/>
Upon compilation, I found no errors. I like how the DataGridView has static state ID and name columns, but I think that the width of the control could be a little wider to show more of the non-static columns.
<br/><br/>
***main***<br/>
I found that the code for the main form was well-documented. In the event for the search button click, the code looks good. However, I would suggest allowing the user to search for any state ID, as there could be more than 50 in the future,
and if the user adds a new state with an ID such as 51, they would be unable to search for it by ID. I like how the checks for the field and operator radio buttons are handled.
<br/><br/>
***Detail***<br/>
The code for the details form is well-documented. The code looks good, but I think the ToString() calls when setting the text for fields like state name or capital are unnecessary because the state object already has them as string properties.
<br/><br/>
***Testing***<br/>
I noticed that when an invalid value, such as “qweu9dj” in the population column, is entered into the DataGridView in the main form, an error message displays that can be hard for a user to read. To fix this, handle the invalid value in the data error event.<br/>
I also discovered that if a state is inserted into the database with a value less than 0 or greater than 255 for its ID or computer job percentage, it will cause the program to crash when the user tries to open the details form for that state.
This error, an overflow exception, occurs when instantiating the new state object that would be passed to the details form constructor.
<br/><br/>
***Comparing Projects***<br/>
In this project, table adapter queries and an entity class were used to select and store the contents of records from the database. In my project, I used LINQ to SQL to select, insert, update, and delete records from the database.
I don’t see any significant differences between the two, only that I find LINQ to SQL easier to work with. Thus, I will continue to use LINQ to SQL in my code when possible.
