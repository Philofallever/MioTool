SOAP
==================
What is a GOAP Agent?

- Goals: The state that the agents want to happen.
    - A state is simply a true or false situation
- Actions: The things the agent can do, each has a set of preconditions, effects and cost
    - A precondition is what determines if an action can be started or not — For example, `To Eat` you need a precondition of `HasFood.`
    - An effect is the result of action and that result can trigger a precondition to be true.  — For example, The `Find Food` action sets the `HasFood` state to be true.
    - A cost is used by the planner to decide between which plan to take. A dynamic cost can help your agents achieve fuzziness logic.
- Planner: Plans a sequence of actions based on the current states to satisfy a goal.
- Agent States and World States, these states are persistent effects.


When an action completes, successfully or not, no after effects will be applied to any of the Agent's states or the World States. It is only used in the planner. 