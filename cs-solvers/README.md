Here you can find solvers for different coding-interview problems from the `itdranik` blog (
[Be](https://www.itdranik.com/be/topics/coding-interview-be),
[Ru](https://www.itdranik.com/ru/topics/coding-interview-ru),
[En](https://www.itdranik.com/en/topics/coding-interview-en)
) in C#.

  - [Caching](#caching)
    - [LFU Algorithm](#lfu-algorithm)
    - [LRU Algorithm](#lru-algorithm)
  - [Parsing Mathematical Expressions](#parsing-mathematical-expressions)
    - [Postfix Notation](#postfix-notation-calculator)
    - [Shunting-Yard Algorithm](#shunting-yard-algorithm)
    - [Tokenization](#tokenization)
  - [Geometry](#geometry)
    - [Max Points on a Line](#max-points-on-a-line)
  - [Game Theory](#game-theory)
    - [Minimax](#minimax)
  - [Graphs](#graphs)
    - [Cycles Checker](#cycles-checker)
    - [Topological Sort](#topological-sort)

## Caching

**Cache** is a software or hardware data storage with high-speed access. For example,
a cache is often used to save calculation results in order to reduce the number of
repeated calculations. A cache can be used to save the results of access to some storage with
a lower access speed.

Typically, every cache interface can be represented with the following set of operations:
  - Add an element by a key;
  - Get an element by a key;
  - Remove an element by a key;
  - Clear the cache.

At the same time, since we can infinitely add elements to the cache only in theory, the cache
should also describe a strategy for discarding elements.

This section contains implementations of different caching eviction (discarding) algorithms.

### LRU Algorithm

**LRU (least recently used)** is the strategy of discarding an element that has not been used the
longest. It is the element that hasn't been accessed by a key the longest (methods of adding and
receiving a value).

Blog post (
[Be](https://www.itdranik.com/be/caching-lru-algorithm-be),
[Ru](https://www.itdranik.com/ru/caching-lru-algorithm-ru),
[En](https://www.itdranik.com/en/caching-lru-algorithm-en)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Caching/LruCache.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Caching/LruCacheTests.cs)

### LFU Algorithm

**LFU (least frequently used)** is the strategy of discarding an element that has been least used.
This element is the element that has been accessed by a key (methods of adding and receiving values)
the least number of times. If there are several such elements, then the element that has not been
accessed the longest is discarded.

Blog post (
[Be](https://www.itdranik.com/be/caching-lfu-algorithm-be),
[Ru](https://www.itdranik.com/ru/caching-lfu-algorithm-ru),
[En](https://www.itdranik.com/en/caching-lfu-algorithm-en)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Caching/LfuCache.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Caching/LfuCacheTests.cs)

## Parsing Mathematical Expressions

This section contains an implementation of simple expression calculator. Check
[Integration tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/IntegrationTests.cs)
for more information please.

### Postfix Notation Calculator

A simple calculator that accepts a sequence of operators and operands in the postfix notation.

Blog post (
[Be](https://www.itdranik.com/be/math-expressions-postfix-notation-be),
[Ru](https://www.itdranik.com/ru/math-expressions-postfix-notation-ru),
[En](https://www.itdranik.com/en/math-expressions-postfix-notation-en)
),
[Implementation](./ITDranik/CodingInterview/Solvers/MathExpressions/PostfixNotationCalculator.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/UnitTests/PostfixNotationCalculatorTests.cs)

### Shunting-Yard Algorithm

A special algorithm for converting from the infix notation to the postfix notation.

Blog post (
[Be](https://www.itdranik.com/be/math-expressions-shunting-yard-algorithm-be),
[Ru](https://www.itdranik.com/ru/math-expressions-shunting-yard-algorithm-ru),
[En](https://www.itdranik.com/en/math-expressions-shunting-yard-algorithm-en)
),
[Implementation](./ITDranik/CodingInterview/Solvers/MathExpressions/ShuntingYardAlgorithm.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/UnitTests/ShuntingYardAlgorithmTests.cs)

### Tokenization

A special class that converts input provided as a string to a sequence of operators and operands
in the infix notation.

Blog post (
[Be](https://www.itdranik.com/be/math-expressions-tokenization-be),
[Ru](https://www.itdranik.com/ru/math-expressions-tokenization-ru),
[En](https://www.itdranik.com/en/math-expressions-tokenization-en)
),
[Implementation](./ITDranik/CodingInterview/Solvers/MathExpressions/Tokenizer.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/UnitTests/TokenizerTests.cs)

## Geometry

This section contains different geometry problem solvers.

### Max Points on a Line

Find the line passing through the maximum number of points.

Blog post (
[Be](https://www.itdranik.com/be/problem-max-points-on-a-line-be),
[Ru](https://www.itdranik.com/ru/problem-max-points-on-a-line-ru),
[En](https://www.itdranik.com/en/problem-max-points-on-a-line-en)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Geometry/MaxPointsOnLine.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Geometry/MaxPointsOnLineTests.cs)

## Game Theory

This section contains different game theory algorithms.

### Minimax

Minimax is an algorithm for minimizing losses when a situation goes in the worst-case scenario.

Blog post (
[Be](https://www.itdranik.com/be/game-theory-minimax-be/),
[Ru](https://www.itdranik.com/ru/game-theory-minimax-ru/),
[En](https://www.itdranik.com/en/game-theory-minimax-en/)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Games/AI/Minimax/Minimax.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Games/TicTacToeMinimaxTests.cs)

## Graphs

This section contains different graphs algorithms.

### Cycles Checker

An auxiliary class for checking a graph for the presence of cycles.

Blog post (
[Be](https://www.itdranik.com/be/graphs-cycles-checker-be/),
[Ru](https://www.itdranik.com/ru/graphs-cycles-checker-ru/),
[En](https://www.itdranik.com/en/graphs-cycles-checker-en/)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Graphs/DigraphHasCyclesCheck.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Graphs/DigraphHasCyclesCheckTests.cs)

### Topological Sort

Topological sort is a linear ordering of graph vertices where each vertex _u_ comes before a
vertex _v_ if there is a directed edge _uv_ from the vertex _u_ to the vertex _v_.

Blog post (
[Be](https://www.itdranik.com/be/graphs-topological-sort-be/),
[Ru](https://www.itdranik.com/ru/graphs-topological-sort-ru/),
[En](https://www.itdranik.com/en/graphs-topological-sort-en/)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Graphs/DigraphTopologicalSort.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Graphs/DigraphTopologicalSortTests.cs)
