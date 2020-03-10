#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace U2U.EntityFrameworkCore
{
  public class ChainOfResponsibility<Body>
  {
    private readonly List<Action<Action<Body>, Body>> handlers
      = new List<Action<Action<Body>, Body>>();

    private readonly Action<Body> endOfLineHandler;

    public ChainOfResponsibility(Action<Body> endOfLineHandler)
      => this.endOfLineHandler = endOfLineHandler ?? throw new ArgumentNullException(nameof(endOfLineHandler));

    public ChainOfResponsibility()
      : this((_) => { })
    { }

    public void Add(Action<Action<Body>, Body> handler)
    => this.handlers.Add(handler);

    public Action<Body> BuildProcessor()
    {
      //handlers.Reverse();
      return ChainHandlers(this.handlers);

      // Handlers contains the list of handlers, with the last element being the first to call
      // So we recursively walk over this list and dynamically create the delegate.
      Action<Body> ChainHandlers(IEnumerable<Action<Action<Body>, Body>> handlers)
      {
        int count = handlers.Count();
        IEnumerable<Action<Action<Body>, Body>> remain = handlers.Skip(1);
        int rCount = remain.Count();
        Action<Body> next = this.endOfLineHandler;
        if (remain.Any())
        {
          next = ChainHandlers(remain);
        }
        global::System.Action<global::System.Action<Body>, Body> handler = handlers.First();
        return (body) => handler(next, body);
      }
    }

    private Action<Body>? processor = null;

    public void Process(Body body)
    {
      this.processor = this.processor ?? this.BuildProcessor();
      this.processor(body);
    }
  }
}
