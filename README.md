FluentHelper
============

A library to help you write objects that can be acted upon in groups with a simple fluid syntax which is more succinct that LInQ alone

Example
------------

Let’s imagine that you have a collection of people.

    List People = new List 
    {
        new Person(){ name="Bentley", birthDate=new DateTime(1963, 3, 7)},
        new Person(){ name="Christy", birthDate=new DateTime(1973, 9, 19)},
        new Person(){ name="Benjamin", birthDate=new DateTime(2001, 9, 19)}
    };

And let’s Imagine that you wanted to get a list of the people who's name begins with the letter B and were borne sine 2000. You might do the following:

    IEnumerable q = People.Where(p => p.name.StartsWith("B")).Where(p => p.birthDate > new DateTime(2000, 1, 1));
Which certainly gets the job done.

Now imagine that you are responsible for creating an maintaining a large list of business rules that look like this. Oh, and we might have some business analysts write the rules to help you out. Suddenly all the p => gets in the way of readability and sharing the task with less technical people.

One solution is a fluent interface. It’s a little more readable. Here is the same query done fluently.

    PersonList q = People.Name().StartsWith("B").BirthDate().Greater(new DateTime(2000,1,1));

Code Layout
------------
*   FluentHelper folder contains the source code for the helper library. You can compile this to a dll to use it in your project.

*   FluentRunner folder conatins a sample to demonstrait a fluent object being created and used.
    *   Person.cs: shows the POCO (Plain Old CLR Object) that is to be made fluent
    *   PersonList.cs: creates a list object and adds the proper FluentCollection types from the FluentHelper library.
    *   Program.cs: demonstraits using the fluent collection
