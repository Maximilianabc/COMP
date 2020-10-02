# Lab 4: String, StringBuilder
This lab will introduce you to the following topics:
* Creating and manipulating strings
* Common string operations
* StringBuilder
* Immutability
* JUnit features

## Common string methods you should know
Check the documentation for more info:
* toLowerCase/toUpperCase
* concatenation (using + operator)
* length
* substring
* indexOf
* equals
* charAt
* replaceAll
* contains

## Immutability of strings
`String` objects are immutable in Java; once created, they cannot be modified. This allows for string interning.
 
 When two strings are created with the same content, they are stored in a "string pool". Therefore, Java can save memory and have both string references refer to the same location in memory. 
```
String str1 = "abc";
String str2 = "abc";
```
In the example above, there is only 1 string object in memory. If we allowed strings to be mutable, then changing one would change the other, potentially causing bugs if we lost track of strings with the same value. However, we can force Java to make new string objects using the `new` operator.

```
String foo1 = new String("foo");
String foo2 = new String("foo");
```

```
str1 == str2 // true, == tracks if they're the same object
str1.equals(str2) // true, checks if the letters match

foo1 == foo2 // false, == tracks if they're the same object. (they're not)
foo1.equals(foo2) // true, checks if letters match
```

For more reasons about why strings were made immutable, check out [this link](https://www.programcreek.com/2013/04/why-string-is-immutable-in-java/).

## StringBuilder
The StringBuilder class lets you treat Strings like objects, with the exception that they can be modified. This makes it more efficient to do string processing operations on them. Afterwards, call the `toString()` method to retrieve the final string.
```
StringBuilder sb = new StringBuilder();     // creates an empty builder
sb.append("olleh");     // adds the letters "olleh" into the builder
sb.reverse();           //reverses the letters internally
String hello = sb.toString();   //"hello"
```
The StringBuffer class is related but not exactly the same as StringBuilder. If you're curious, read about the differences [here](https://stackoverflow.com/questions/355089/difference-between-stringbuilder-and-stringbuffer).

## What you need to do
The detailed description of each TODO task is given in the javadoc comments. 
Fill out the functions in the StringLab class. Then, test your code with the provided JUnit test cases.

## Useful links
* [CodingBat additional practice](http://codingbat.com/java/String-1)
* [String Javadoc](https://docs.oracle.com/javase/10/docs/api/java/lang/String.html)