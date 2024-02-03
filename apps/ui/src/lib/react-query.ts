import { QueryClient, DefaultOptions } from "@tanstack/react-query";

const queryConfig: DefaultOptions = {
  queries: {
    useErrorBoundary: false,
    refetchOnWindowFocus: false,
    retry: false,
  },
};

const env = process.env.NODE_ENV;

export const queryClient = new QueryClient({
  defaultOptions: queryConfig,
  logger: {
    error: env === "test" || env === "production" ? () => {} : console.error,
    log: env === "test" || env === "production" ? () => {} : console.log,
    warn: env === "test" || env === "production" ? () => {} : console.warn,
  },
});
