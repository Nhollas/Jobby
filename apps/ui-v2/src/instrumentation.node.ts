import { getNodeAutoInstrumentations } from "@opentelemetry/auto-instrumentations-node"
import { OTLPTraceExporter } from "@opentelemetry/exporter-trace-otlp-http"
import { Resource } from "@opentelemetry/resources"
import { NodeSDK } from "@opentelemetry/sdk-node"
import {
  BatchSpanProcessor,
  SpanProcessor,
} from "@opentelemetry/sdk-trace-node"
import { ATTR_SERVICE_NAME } from "@opentelemetry/semantic-conventions"

const isTracingDisabled =
  process.env.NEXT_PUBLIC_OTEL_COLLECTOR_URL === "disabled"

export function defaultSpanProcessor(): SpanProcessor {
  const exporter = new OTLPTraceExporter({
    url: isTracingDisabled
      ? undefined
      : process.env.NEXT_PUBLIC_OTEL_COLLECTOR_URL,
  })

  return new BatchSpanProcessor(exporter)
}

const sdk = new NodeSDK({
  resource: new Resource({
    [ATTR_SERVICE_NAME]: "Create_Nhollas_App.Backend",
  }),
  spanProcessors: [defaultSpanProcessor()],
  instrumentations: [
    getNodeAutoInstrumentations({
      "@opentelemetry/instrumentation-fs": {
        enabled: false,
      },
      "@opentelemetry/instrumentation-net": {
        enabled: false,
      },
    }),
  ],
})

try {
  if (!isTracingDisabled) {
    console.info("Starting OpenTelemetry SDK...")
    sdk.start()
    console.info("OpenTelemetry SDK started successfully.")
  } else {
    console.warn(
      'Tracing is disabled because NEXT_PUBLIC_OTEL_COLLECTOR_URL is set to "disabled".',
    )
  }
} catch (error) {
  console.error("Failed to initialize OpenTelemetry SDK:", error)
}
