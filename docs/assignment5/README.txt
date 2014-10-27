README
CPTS 422
Assignment 5
10/26/2014
Team 2 - (Lansdon Page, Jason Wong, Jason Stidham, Ryan Wilson)

This readme file is a guide to the deliverables for assignment 5:

State Model Testing

1) Test Plan - Found in https://github.com/lansdon/cpts422/blob/master/docs/assignment5/Test%20Plan.docx

2) State Transition Diagrams - Found in:
a) Running Transition Diagrams: 

b) Parser Transition Diagrams: https://github.com/lansdon/cpts422/blob/master/docs/assignment5/Parser%20State%20Transition%20Diagram.docx

3) State Transition Trees
a) Running Transition Diagrams: https://github.com/lansdon/cpts422/blob/master/docs/assignment5/running_transition_diagram.pdf
b) Parser Transition Diagrams: https://github.com/lansdon/cpts422/blob/master/docs/assignment5/parse_transition_tree_diagram.pdf

4) Complete Round Trip - Test Suites derived from state transition trees.
a) Run States: https://github.com/lansdon/cpts422/blob/master/docs/assignment5/roundTripRunState.pdf
b) Parser: https://github.com/lansdon/cpts422/blob/master/docs/assignment5/roundTripParse.pdf

5) *Correct* Answers to questions:

1. What kinds of defects does round-trip testing reveal? Note: you are essentially establishing the fault model for state model testing.

By traversing the paths that reach every state, the software can be tested to make sure all paths and states work together in a sequence. This can be considered a fault model because you’re identifying areas of the software that pertain to different states and how those states affect each other and how they affect to program when they change.

2. Does state model testing replace combination-based testing? Explain.

Combination testing is often more of a zoomed in perspective of a particular module or function and covers the different possible ways that function can be used. It provides more specific tests and potentially covers edge cases that are not easily considered from state testing. As an example, our combination testing of the parser focused on a single transition of the parser state diagram and was not concerned with how that transition interacted with the system as a whole. It was focused on making sure that transition functioned properly for any given input. So “No”, I don’t think it’s a direct replacement for combination testing. State testing is  a zoomed out perspective where combination testing can be more focused on a smaller piece.

3. Does state model testing replace unit testing? Explain.

For the similar reasons as #2, I don’t feel state model testing would replace unit testing. Unit testing is also focused on smaller pieces where the state model testing is more concerned with how the pieces work when sequenced together in the context of the application as a whole. For example, in our parser code, there are 10 classes involved in the complete action of parsing a definition file. It starts from the TM class, and visits each component of the application to parse the portion of the definition specific to that class. Unit testing would deal with each of those classes individually, where as state model testing deals with visiting all 10 classes in a sequence according to the state model and validates that the parsing system as a whole functions properly.

