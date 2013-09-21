MVC4 Blog Application
===================

This application investigates the C# MVC4 framework in create a basic CRUD (Create Read Update Delete) application that
will maintain blog posts and user comments. This project exists in order investigate the full end to end development process of
C# MVC4. This application is purely investigative and does not represent a real world implementation.

###Technologies

Some of the technologies used within this project are;

- [TypeScript](http://www.typescriptlang.org/)
- Entity Framework
- MSSQL

###Gotchas

- Data Migrations - When performing `enable-migrations` within the `Package Manager Console` ensure you have selected
the correct default project. Otherwise the errors will suggest you need Entity Framework installed, which may be unexpected
if you have the wrong default project selected.
Further to this you will need to run `update-database` if you check this out for the first time for instance.
[As explained here](http://stackoverflow.com/questions/11923077/the-entityframework-package-is-not-installed-on-project)
- The `[ValidateAntiForgeryToken]` attribute needs to be placed both in a controller, and within the form that HttpPosts to the
controller. For example;

```
@using (Html.BeginForm()) {
    @Html.AntiForgeryToken()
```
- Having the `DatabaseGenerated(DatabaseGeneratedOption.Computed)` annotation with the Datetime property is not enough to trigger default insert values.
This needs to be provided either manually or through a Migration file.

###Useful Visual Studio Extensions

- Entiy Framework Power Tools - DbContext visualisation
- Web Essentials - Useful for TypeScript transpiling on file save
- Visual Studio Tools for Git

###Conclusion

Using C# MVC4 proved to be extremely flexible and productive. I would certainly make use of the frameworks again in the future.
In comparison to my Scala Play Blog APplication  I found the DAL to be a lot easier to handle using the Microsoft Entity Framework in
comparison to Slick. However I found the compile time errors of Play's Model and View layer to be extremely time saving in comparison to
the runtime errors which existed with MVC4.
