Here you can find solvers for different coding-interview problems from the `itdranik` blog (
[Be](https://www.itdranik.com/be/categories/coding-interview),
[Ru](https://www.itdranik.com/ru/categories/coding-interview),
[En](https://www.itdranik.com/en/categories/coding-interview)
) in C#.

- [Caching](#caching)
  - [LRU Algorithm](#lru-algorithm)
  - [LFU Algorithm](#lfu-algorithm)
- [Parsing Mathematical Expressions](#parsing-mathematical-expressions)
  - [Postfix Notation Calculator](#postfix-notation-calculator)
  - [Shunting-Yard Algorithm](#shunting-yard-algorithm)
  - [Tokenization](#tokenization)
- [Geometry](#geometry)
  - [Max Points on a Line](#max-points-on-a-line)
- [Game Theory](#game-theory)
  - [Minimax](#minimax)

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
[Be](https://www.itdranik.com/be/post/caching-lru-algorithm),
[Ru](https://www.itdranik.com/ru/post/caching-lru-algorithm),
[En](https://www.itdranik.com/en/post/caching-lru-algorithm)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Caching/LruCache.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Caching/LruCacheTests.cs)

### LFU Algorithm

**LFU (least frequently used)** is the strategy of discarding an element that has been least used.
This element is the element that has beeb accessed by a key (methods of adding and receiving values)
the least number of times. If there are several such elements, then the element that has not been
accessed the longest is discarded.

Blog post (
[Be](https://www.itdranik.com/be/post/caching-lfu-algorithm),
[Ru](https://www.itdranik.com/ru/post/caching-lfu-algorithm),
[En](https://www.itdranik.com/en/post/caching-lfu-algorithm)
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
[Be](https://www.itdranik.com/be/post/math-expressions-postfix-notation),
[Ru](https://www.itdranik.com/ru/post/math-expressions-postfix-notation),
[En](https://www.itdranik.com/en/post/math-expressions-postfix-notation)
),
[Implementation](./ITDranik/CodingInterview/Solvers/MathExpressions/PostfixNotationCalculator.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/UnitTests/PostfixNotationCalculatorTests.cs)

### Shunting-Yard Algorithm

A special algorithm for converting from the infix notation to the postfix notation.

Blog post (
[Be](https://www.itdranik.com/be/post/math-expressions-shunting-yard-algorithm),
[Ru](https://www.itdranik.com/ru/post/math-expressions-shunting-yard-algorithm),
[En](https://www.itdranik.com/en/post/math-expressions-shunting-yard-algorithm)
),
[Implementation](./ITDranik/CodingInterview/Solvers/MathExpressions/ShuntingYardAlgorithm.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/UnitTests/ShuntingYardAlgorithmTests.cs)

### Tokenization

A special class that converts input provided as a string to a sequence of operators and operands
in the infix notation.

Blog post (
[Be](https://www.itdranik.com/be/post/math-expressions-tokenization),
[Ru](https://www.itdranik.com/ru/post/math-expressions-tokenization),
[En](https://www.itdranik.com/en/post/math-expressions-tokenization)
),
[Implementation](./ITDranik/CodingInterview/Solvers/MathExpressions/Tokenizer.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/MathExpressions/UnitTests/TokenizerTests.cs)

## Geometry

This section contains different geometry problem solvers.

### Max Points on a Line

Find the line passing through the maximum number of points.

Blog post (
[Be](https://www.itdranik.com/be/post/problem-max-points-on-a-line),
[Ru](https://www.itdranik.com/ru/post/problem-max-points-on-a-line),
[En](https://www.itdranik.com/en/post/problem-max-points-on-a-line)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Geometry/MaxPointsOnLine.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Geometry/MaxPointsOnLineTests.cs)

## Game Theory

This section contains different game theory algorithms.

### Minimax

Minimax is an algorithm for minimizing losses when a situation goes in the worst-case scenario.

Blog post (
[Be](https://www.itdranik.com/be/post/game-theory-minimax/),
[Ru](https://www.itdranik.com/ru/post/game-theory-minimax/),
[En](https://www.itdranik.com/en/post/game-theory-minimax/)
),
[Implementation](./ITDranik/CodingInterview/Solvers/Games/AI/Minimax/Minimax.cs),
[Tests](./ITDranik/CodingInterview/SolversTests/Games/TicTacToeMinimaxTests.cs)
