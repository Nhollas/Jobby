# create-nhollas-app

## Setting Up The Project

#### Prerequisites

- Node.js
- npm
- Docker (if you want to run the OpenTelemetry Collector)

You can check your Node.js and npm versions by running:

```bash
node --version
npm --version
```

#### 1. Install Dependencies (if not done already through CLI):

```bash
npm install
```

#### 2. Install Playwright:

```bash
npx playwright install --with-deps
```

#### 3. Setup environment variables:

**copy the `.env.example` file to a new file called `.env.local`.**

```bash
# Windows
copy .env.example .env.local

# Mac
cp .env.example .env.local
```

#### 4. Run Development Server

Finally, run the development server:

```bash
npm run dev
```

Now you can open [http://localhost:3000](http://localhost:3000) with your browser to see the application.

## Running Tests

#### Playwright

To run playwright tests, run the following command:

```bash
npm run test:e2e
```

This will build the application and run the playwrigh tests.

Note: You don't need to repeatedly build the application once you've done it once. Running the above command at the start will set you up to then simply run:

```bash
npm run playwright
```

For subsequent test runs or `npx playwright test` if you prefer playwrights cli.

#### Vitest

To run the unit tests with Vitest, run the following command:

```bash
npm run test:unit
```

## Running OpenTelemetry Collector

#### 1. Start the Jaeger and OpenTelemetry Collector services using Docker Compose:

```sh
docker-compose -f infra/docker-compose.yaml up -d
```

This will start the necessary tracing infrastructure in the background.

#### 2. Viewing Jaeger Trace UI

To view the Jaeger Trace UI, open your browser and navigate to:

```
http://localhost:16686
```

## Available Scripts

| Script                  | Description                                                                                                         |
|-------------------------|---------------------------------------------------------------------------------------------------------------------|
| `npm run dev`           | Starts the Next.js development server (with Turbopack enabled). Automatically rebuilds on code changes.             |
| `npm run build`         | Builds a production-ready version of your Next.js application.                                                      |
| `npm run start`         | Runs the previously built application in production mode.                                                           |
| `npm run test`          | Runs both unit tests (via Vitest) and end-to-end tests (via Playwright). Internally calls `test:unit` + `test:e2e`. |
| `npm run test:unit`     | Runs **Vitest** unit tests with a verbose reporter.                                                                 |
| `npm run test:e2e`      | Builds the app, then runs **Playwright** tests for end-to-end coverage. Internally calls `build` + `playwright`.    |
| `npm run playwright`    | Directly runs **Playwright** tests (skips the build step).                                                          |
| `npm run test:coverage` | Runs Vitest with coverage enabled.                                                                                  |
| `npm run lint`          | Runs ESLint to identify and automatically fix linting issues.                                                       |
| `npm run format`        | Uses Prettier to format your code according to your project’s style rules.                                          |
| `npm run typecheck`     | Runs the TypeScript compiler (`tsc`) in noEmit mode to check for type errors.                                       |
| `npm run validate`      | Checks your code by running `typecheck`, `lint`, and `format` sequentially.                                         |
| `npm run validate:test` | Performs all checks (type, lint, format) and then runs the entire test suite.                                       |