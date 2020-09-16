Intro
=====

We are given some code from a coworker who wants to implement observability on the users game state.

The simple cases work fine but we are facing a problem when multiple properties are changed. 
This is demonstrated in a test case `TestSuite.CanObserveConsistentGameStateChanges` that you can run in the
test runner within unity.

The issue is that observers will get called while the game state is within a transaction. So they will
observe in-between values.

Objective
=========

Main objective: Implement a system that supports observability on game state properties but makes sure 
that observers will only see consistent game states. 

The main objective should be achieved for all (future) properties. So the goal is not to find a solution
that will only work for the specific problem shown in the test.

This means that we want to be able to register multiple observers for different game state properties 
and they should all be called only after the game state is in a consistent state but only when a property
changed that they actually observe. So calling all observers for all changes to the game state is not 
good enough.

An interesting additional feature could be that observers could register for multiple properties but are only 
notified once even when both are changed.

Expectations
============

The main point of the assignment is to get an idea of your skills to design and implement a solution. 
So please don't try to create something that just barely works with the minimal amount of lines of code. 
That would defeat the purpose.

Any solution that would fix the broken test case is a valid one. You are allowed to change the API 
as long as the main objective is accomplished: Being able to observe property changes when the game state
is in a consistent state.

Take into consideration that a real game state will be much more complex. So it will contain a lot 
more properties. It will also contain more complex substructures like lists and non-trivial types.

Please concentrate on the observable problem and ignore other issues with the code. I.e. don't introduce 
dependency injection just to get rid of the singletons.

Your solution should be something that you would actually propose to your team. So it should have 
the quality you expect from a pull request. We will evaluate your code in this way. So we will assume that 
this is an actual real world sample of what you would want to check into the repository. 

This is really important:

When we look at your code we will not think: Ah you would not have pushed this in a real project.
We will take this as it is.

So please make sure that you do the best you can do. Take your time. We know that you might have other
things to do. So it is entirly ok if you submit your result 7 days after you got the assignment.
If you need more time please just drop us a note so that we know that you are still interested.

Thanks and happy coding!

We are looking forward to your solution and hopefully to meet you soon!

 
