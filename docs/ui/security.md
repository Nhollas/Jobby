# 🔐 Security

## Authentication

This project uses [Clerk](https://clerk.com) for it's security. This alone does pretty much all the heavy lifting in terms of this projects security requirements. The key requirement here was that I needed to make sure a user can only receive/edit data they own.

For this problem I solely used Clerks out of the box solution to generate [JWT's](https://clerk.com/docs/backend-requests/making/jwt-templates#jwt-templates) for my users. These JWT's contain the users credentials and are signed by Clerk. This way I can verify that the user is who they say they are and that they are allowed to access the data they are requesting on my backend.

## Using the Backend For Frontend pattern (BFF)

While I generate JWT's for my users, I don't want to expose these to the frontend. This is where the BFF pattern comes in. I utilise Nexts [router handlers](https://nextjs.org/docs/app/building-your-application/routing/route-handlers) to work as a middleman for the requests that go to my backend. So from the users point of view they are making a request to the frontend, but in reality the frontend is making a request to the backend. This way I can keep my JWT's safe and secure.

[Pattern in Action](../../apps/ui/src/app/api/[...slug]/route.ts)

## Tooling:

#### [Clerk Auth](https://clerk.com/docs)

Clerk is a auth provider that handles authentication and authorization as a service. It is very easy to use and provides a lot of features out of the box. It is also very flexible and can be configured to fit your needs.
