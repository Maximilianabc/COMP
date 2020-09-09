# Lab 1: IntelliJ + Git Quick Start Guide
This lab covers the following topics:
* Installing and running IntelliJ IDEA 2020.x with Java 14
* Creating a Hello World program
* Using git with GitHub for version control
* A sneak peak of assignment 1

## JDK 14 download and installation
IntelliJ comes bundled with Java 8 but this is not the version we want. Therefore, we must Download and install JDK 14 [here](https://www.oracle.com/java/technologies/javase-jdk14-downloads.html). **Note the install location**, as it will be useful later.

## IntelliJ Installation
IntelliJ IDEA comes in two versions, Community and Ultimate. The Community version is good enough for this course, but you can apply for a student Ultimate license [here](https://www.jetbrains.com/student/). Follow the download and installation instructions [here](https://www.jetbrains.com/idea/download/). 

## Hello World in IntelliJ
1. Launch IntelliJ and create a new project

2. In the Project SDK bar, select the `jdk-14.0.2.` folder we installed from the previous section, and click next. (For reference, mine was located at `/Library/Java/JavaVirtualMachines/jdk-14.0.2.jdk/`)

3. Check "Create project from template", click Next

4. Name your project, select a location, and click Finish

5. Replace the `// write your code here` in `Main.java` with the following statement

   ```
   System.out.println("Hello, World!");
   ```

6. Click the green play button in the top right corner to run the program. After a couple of seconds, a terminal should pop up saying Hello World.

To view the project files, on the left sidebar, click the "Project" button. The directory view is very similar to the view in Eclipse. At the bottom bar, click the "TODO" button to view any `//TODO` comments left by the TA throughout the code.

### Testing a Java 14 feature

In the main function, add the following lines.

```
String tempString = "Hello";
if (tempString instanceOf String newString){
	System.out.print(newString);
}
```

If the language level of your project is not set properly, IntelliJ will highlight the codes in red. 

Click on it, follow the hints (or right click on the module in the left sidebar, then select "Open Module Settings") to go to `Module Settings` , and set the project language level to `14(Preview)-Records, Patterns, Text blocks`. Now, everything is fine and you should be able to run the codes. 

[Note: The project language level selection can also be made at `File > Project Structure > Project Settings > Project`.]



### Useful IntelliJ Shortcuts
* In your code, click on an error and press `alt`+`enter` for suggestions on how to fix the error
* `ctrl` + `alt` + `L` to automatically fix code formatting
* After clicking on a function, press `ctrl` + `alt` + `b` to jump to the implementation 
* `ctrl` + `shift` + `F` to search
* `ctrl` + `R` for find/replace menu

## Using git and GitHub
### GitHub
We will be using git for version control, and GitHub to store the code remotely. In order to create private code repositories on GitHub, you'll need to register for a free student pack [here](https://help.github.com/articles/applying-for-a-student-developer-pack/). Once your application is approved, create a private repository like [this](https://help.github.com/articles/create-a-repo/). Note the repository URL, e.g. `https://github.com/lamngok/helloworld.git`

### git
In your command prompt, navigate to the root directory for the Hello World program we just created. Type the following commands.  
* `git init`, which creates a git repo here
* `git add .`, which adds all the files into the repo
* `git commit -m "first commit"`, which commits the files being tracked
* `git remote add origin <YOUR REPOSITORY URL>`, which tells git where the remote repository is, and names it origin. Substitute your repository URL accordingly.
* `git push -u origin master`, which pushes your master branch to origin, in a branch also called master.  

Optionally, you may create a `.gitignore` to avoid tracking unnecessary files. This doesn't really matter, but you can Google it on your own.

### Useful commands
Here's a [list](https://confluence.atlassian.com/bitbucketserver/basic-git-commands-776639767.html) of useful git commands.



## IntelliJ vs. Eclipse

 There's plenty of articles online detailing the objective benefits of IntelliJ over Eclipse. Having used both, here's a list of subjective personal opinions.
 * Better refactoring
 * Better searching
 * Better window layouts
 * Better autocomplete, code simplification suggestions



## Lab 1 Outcome submission

1.Import our prepared **Demo1** into IntelliJ, take a **screenshot**

2.Create a Hello World project

3.Create a **private** repository on GitHub (Get an **education account** first)

4.Create a local Git repository for your Hello World project

5.Add your GitHub repository as a remote repository for your Hello World project

6.Push your Hello World to your private repository on GitHub, take a **screenshot** of your GitHub repository

7.Submit a Word or PDF file including a **screenshot** of your imported IntelliJ project and a **screenshot** of your created Git repository on GitHub to [CASS](https://course.cse.ust.hk/cass)