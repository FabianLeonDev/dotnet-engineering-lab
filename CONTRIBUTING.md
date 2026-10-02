# Contributing

Thanks for contributing to the .NET Engineering Lab. Contributions should help readers understand behavior, trade-offs, performance, or practical implementation details through focused, reproducible work.

## Getting started

1. Check the existing issues and experiments to avoid duplicating work.
2. For a substantial change, open an issue first and agree on the problem and scope before implementation.
3. Fork the repository, then clone your fork and create a branch from the repository's default branch.
4. Keep each change focused. Experiments should be isolated and placed in the area that best matches the topic described in the [README](README.md).
5. Run the relevant checks and open a pull request against the default branch.

## Creating an issue

On GitHub, open **Issues**, choose **New issue**, and select the form that best matches the work:

- **Bug** for unexpected behavior. Include observed and expected behavior, reproduction steps, and evidence where possible.
- **Feature** for a new capability or improvement. Describe the problem, goal, requirements, and acceptance criteria.
- **User Story** for a user-centered capability. State the actor, capability, and value, then provide acceptance criteria.
- **Technical Task** for implementation or maintenance work. Describe the motivation, goal, scope, acceptance criteria, and validation.
- **Experiment** for an investigation. State a focused question and hypothesis, then describe the setup, variables, measurements, expected evidence, and limitations.

Use concrete, testable acceptance criteria. Keep the scope small enough to review and complete independently.

## Branch naming

Use a short, lowercase, kebab-case description, optionally preceded by an issue number. Start the branch name with the type of work:

```text
<type>/<issue-number>-<short-description>
```

Examples:

```text
feat/42-async-streaming-demo
fix/57-cancellation-race
experiment/81-array-pooling
docs/96-contribution-guide
```

Do not include spaces or unrelated changes on a branch. Omit the issue number when no issue exists.

## Conventional Commits

Format commit messages as:

```text
<type>(<optional-scope>): <short description>
```

Use a lowercase type and a concise description. Common types for this repository include:

- `feat`: add a user-facing capability or experiment
- `fix`: correct a defect
- `docs`: documentation-only changes
- `test`: add or change tests
- `refactor`: restructure code without changing behavior
- `perf`: improve performance
- `build`: change build tooling or dependencies
- `ci`: change continuous integration configuration
- `chore`: repository maintenance that does not fit another type

Examples:

```text
feat(async): add bounded channel experiment
fix(memory): dispose benchmark resources
experiment(concurrency): compare lock strategies
docs: add contribution guide
chore: add GitHub issue forms
```

Keep commits focused and avoid vague messages such as `update` or `fix stuff`. For a breaking change, add `!` after the type or scope and explain the change in the commit body, for example `feat(api)!: change result format`.

## Changes and validation

- Follow the existing structure and conventions in the affected area.
- Prefer small, self-contained changes. Explain non-obvious design choices and trade-offs in the pull request.
- For code changes, build and test the affected solution or project. For example, run `dotnet build <path-to-solution-or-project>` and `dotnet test <path-to-solution-or-project>`, replacing the placeholder with the relevant path. This repository may not have a solution or project at its root, so do not assume these commands apply repository-wide.
- For experiments, record enough environment and procedure detail to make results reproducible. Report measurements and limitations; do not present an unverified expectation as a result.
- For documentation-only changes, check links, spelling, and rendered Markdown.
- If a check cannot be run, state why and include any manual validation performed.

## Pull requests

- Open the pull request against the repository's default branch.
- Use a clear title that follows the Conventional Commit format where practical.
- Summarize the problem, the approach, and any relevant trade-offs.
- List the validation performed and its results. Include benchmark setup and results when relevant.
- Link the related issue using `Closes #<issue-number>` when the pull request completes it.
- Keep the pull request focused; split unrelated work into separate issues or pull requests.
- Respond to review feedback and rerun relevant checks after making changes.
