# Lab 7: Abstract Classes, Interfaces, Nested classes
This lab will cover the following topics:

* Abstract Classes
* Interfaces (and default/private interface methods)
* Nested classes

## Interfaces
Interfaces define a common behavior for classes which implement the interface. Think of it as an "contract" to fulfill any of the methods in the interface. Commonly, you'll see interfaces with the suffix "-able", e.g. Comparable, Cloneable, Autocloseable, etc. Classes may implement multiple interfaces.   

Useful information about interfaces:

* Contains only public static final variables, even if you don't write the keywords
* Contains only public abstract methods, even if you don't write the keywords (before Java 8)
* May also contain default, static, private, private static methods (after Java 8)
    * Default methods DO have an implementation, unlike abstract methods
* No constructors; you can't instantiate an interface, because you can't instantiate a "contract". Rather, classes that fulfill this contract can be instantiated.
* A class can only extend one class but can implement mutiple interfaces

```
public interface Haha{
        public abstract void laughing();
}
public class Hehe implements Haha {
        public void laughing(){
                System.out.println("lol");
        }
}
//in another file...
public static void main (String[] args){
        Haha a = new Haha(); //BAD
        Haha b = new Hehe(); //OK
}
```

* Interfaces can extend other interfaces
```
interface Monster {
	void menace();
}
interface DangerousMonster extends Monster {
	void destroy();
}
interface Vampire extends DangerousMonster, Lethal {
	void drinkBlood();
}
```

### Common interfaces
* `Comparable<T>`: Classes which implement this interface must implement the `compareTo` function, which allows us to compare (and sort) objects of this type
* `List<T>`: Classes which implement this interface can be treated like lists, e.g. ArrayList and LinkedList.

### Default interface methods
Java 8 introduced default interface methods, allowing developers to add new methods to interfaces without breaking older implementations of these interfaces.
```
public interface Foo {
    void bar(); //is public abstract
    
    //Default method declaration
    default int sumDefault(int a, int b) {
        return a + b; //simply adds a + b
    }
    
    //Static method declaration
    static int sumStatic(int a, int b) {
        return a + b; //simply adds a + b
    }
}
```

### Private interface methods
Java 9 introduced private interface methods, which can be used as helper functions within the interface. Some notes on private interface methods:
* They cannot be abstract
* Can only be used inside the interface (hence the term `private`)
* Private non-static methods can't be used inside private static methods

```
public interface MyInterface {
    default void defaultMethod() {
        privateMethod("Hello from the default method!");
    }
    private void privateMethod(String string) {
        System.out.println(string);
    }
    void normalMethod();
}
```

## Abstract Classes
Typically, the purpose of an abstract class is to define some common behavior that can be inherited by multiple subclasses. An abstract class is any class explicitly declared with the `abstract` modifier. It's ok for an abstract class to have 0 or more abstract methods (unlike C++, which requires >=1 pure virtual function(s)).  

An abstract method is abstract only if the body is empty, and it contains the `abstract` modifier. Otherwise, it's a concrete method. Abstract methods may only be declared in an abstract class.  

Useful information about abstract classes:
* Cannot be instantiated with the `new` operator
* Cannot have private abstract methods (because nobody can see it, and nobody can implement it)
* CAN have constructors (which are invoked when subclass constructor is invoked)
* Subclass can be abstract even if superclass is concrete
    * All classes inherit from the concrete `Object` class! 
* Subclass can override a method from its superclass, and define it as abstract

```
class Haha{
        public void desmond(){…}
}

abstract class Hehe extends Haha{
        public abstract void desmond();
} //OK

```

### Comparison
An interface is simply a contract describing the behavior the implementing class should have. There is no other guarantee of similarity between the implementing classes.  
An abstract class is the basis for different subclasses that share similar behavior. Subclasses which extend from the abstract class also have the option to override the predefined behavior, making it subclass-specific.  

In other [words](https://stackoverflow.com/questions/18777989/how-should-i-have-explained-the-difference-between-an-interface-and-an-abstract):

* An interface defines a contract that some implementation will fulfill for you
* An abstract class provides a default behavior that your implementation can reuse

## Nested classes
A nested class is a class defined inside a class. They let us logically organize our classes and increase encapsulation.  

Nested classes are divided into two categories: static and non-static. 
* Static nested classes are just called static nested classes
* Non-static nested classes are called inner classes

### Static nested classes
Static nested classes belong to the enclosing class, not an object (hence the `static` keyword). 
* Static nested classes can only access static members of the enclosing class. 
* You don't need an instance of the enclosing class to create an instance of the static nested class.
* The enclosed and enclosing class have private level access to each other

```
public class Enclosing {
    private static int staticX = 1;
    private int nonStaticX = 2;

    public static class StaticNested {
        private void run() {
            System.out.println(staticX); //OK, even though private
            System.out.println(nonStaticX); //ERROR, since we only have access to the class, not an instance
        }
    }

    public static void main(String[] args) {
        //don't need instance of Enclosing class
        Enclosing.StaticNested nested = new Enclosing.StaticNested(); //or use var to save effort!
        nested.run(); //can access private method
    }
}
```
### Inner classes
Inner classes are non-static nested classes. 

* They belong to an instance of the outer class
* Inner classes can access both static and non-static members of the outer class
* You need to instantiate the outer class before you can instantiate the inner class
* Inner classes cannot have static members
```
public class Outer {
    private static int staticX = 1;
    private int nonStaticX = 2;

    public class Inner {
        private int y = 3; //OK
        private static int z = 4; //ERROR, cannot have static members

        private void run() {
            System.out.println(staticX); //OK, even though private
            System.out.println(nonStaticX); //OK, even though private
        }
    }

    public static void main(String[] args) {
        Outer.Inner nested = new Outer.Inner(); //error!
        Outer.Inner nested2 = (new Outer()).new Inner(); //OK, since we instantiate Outer first
        nested2.run(); //can access private method
    }
}
```
#### Local classes
Local classes are a special type of inner class defined inside a method or scope block.
* They cannot have access modifiers in their declaration
* They have access to both static and non-static members in the enclosing context
* They can only define instance members
```
public class Outer {

    void foo(){
        private class Local{} //ERROR, no modifiers allowed
    }

    private static int x = 5;
    private int y = 10;

    void bar() {
        class Local {
            void run() {
                System.out.println(x); //OK, even though private static
                System.out.println(y); //OK, even though private
            }

            static void badRun(){} //ERROR, no static members
        }
        Local local = new Local();
        local.run();
    }

    public static void main(String[] args) {
        Outer newOuter = new Outer();
        newOuter.bar();
    }
}
```
#### Anonymous classes
Anonymous classes can be used to:
* Create an object which implements an interface
* Create an object which extends a class

```
interface Age {
    int x = 21;
    void getAge();
}

class AnonymousDemo {
    public static void main(String[] args) {
        Age oj1 = new Age() { //an anonymous class which implements Age interface
            @Override
            public void getAge() {
                System.out.print("Age is " + x); //can access x!
            }
        };
        oj1.getAge();
    }
}
```

```
class MyThread {
    public static void main(String[] args) {
        //anonymous classes can even go inside function arguments expecting an interface type
        //anonymous class is implementing Runnable interface
        Thread t = new Thread(new Runnable() {
            public void run() {
                System.out.println("Child Thread");
            }
        });
        t.start();
    }
}
```

There are several notable differences between anonymous and normal classes:

* Anonymous classes can only implement 1 interface at a time, normal classes >= 0
* Anonymous classes can either extend a class or implement an interface, but not both
* Anonymous classes can't have explicitly defined constructors (since you don't have access to the class name)

Note the following regarding how anonymous classes can access local variables:

* Anonymous classes have access to the members of its enclosing class
* Anonymous classes cannot access local variables in its enclosing scope that are not declared as final or effectively final
    * Effectively final: a variable that you could add the `final` keyword to but the compiler wouldn't complain, since its value is never changed again
```
class MyThread {
    int x = 6;
    int nonFinalX = 7;

    public void foo() {
        int y = 8;
        int nonFinalY = 9;

        nonFinalX += 1;
        nonFinalY += 1;
        
        Thread t = new Thread(new Runnable() {
            public void run() {
                System.out.println(x); //OK, since it's not a local var
                System.out.println(y); //OK, can access, since it's an effectively final local var
                System.out.println(nonFinalX); //OK, since it's not a local var
                System.out.println(nonFinalY); //ERROR, since it's not effectively final but a local var
            }
        });
    }
}
```

##### Lambdas and anonymous classes
Lambdas can be used to replace anonymous classes when they're used to implement an interface with a single method. We'll cover it in future labs, but you may have a [peek ahead first](https://www.oreilly.com/learning/java-8-functional-interfaces).

### Shadowing
The declaration of the members of an inner class shadow those of the enclosing class if they have the same name. See [the official docs](https://docs.oracle.com/javase/tutorial/java/javaOO/nested.html#shadowing) for more info.

## What you need to do
In this lab, you will implement a prefix calculator system shown in the following diagram:  
![diagram.png](diagram.png)  
The `structure.Expression` class is the root abstract class. All math operations are done on expressions. 

Most of the code has already been provided for you.
Detailed information is provided in the Javadoc comments. 
Complete the TODOs, and run the provided test cases.

## Lab Outcome & Submission

* Import the project
* Finish the TODOs and make sure that your implementation passes the provided test (ExpressionTest)
* Compress the whole project and submit to CASS
