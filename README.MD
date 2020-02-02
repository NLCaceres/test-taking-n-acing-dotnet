# Test Taking 'n Acing (Name a work in progress)

As with many apps out there, it's tough to find that one app that has each and every feature out there. In the case of practice test apps, I often find that
the layout is too bare bones, a simple CSV or word document upload would help to add tests you already have, and that some prefer flash card style over basic
stylized testing when they could be combined. It's my hope to use Blazor WebAssembly along with a MongoDB serving ASP.Net Web API to create a nice easy to access website for whenever someone needs to practice for the big upcoming test. Ideally once the Web API goes up, a React Native app could be thrown together
to serve as both the Android and iOS apps

## Upcoming Ideas

- Branching out: Currently thinking MongoDB, but will also try Postgresql served data
- React Native Version
- Update Models to handle different types of test questions
- XML, CSV, JSON, Word Doc based upload of tests.

## Note about CRLF

- In Unix systems end of lines are represented by LF (line feed). Windows on the other hand uses CR (carriage return) followed by an LF. When pushing it up to
  Github, the push (on a UNIX system) will force it to be written as simply an LF. Depending on the system that pulls any changes down, you may end up with something entirely different than originally intended. For this reason, 'git config --global core.autocrlf false' is recommended these days. Keeping CRLFs as is and preventing any weird automagic changes.
