import { FileCode2 } from "lucide-react"
import { Button } from "@/app/components/ui/button"
import Image from "next/image"
import Link from "next/link"

export default function Home() {
  return (
    <div className="min-h-screen bg-black text-white">
      {/* Hero Section */}
      <div className="relative border-b border-white/10">
        <div className="absolute inset-0 bg-gradient-to-b from-violet-600/10 to-transparent" />
        <div className="absolute inset-0 rotate-180 bg-[radial-gradient(ellipse_at_top,_var(--tw-gradient-stops))] from-blue-600/30 via-transparent to-transparent" />
        <div className="relative mx-auto flex max-w-screen-sm flex-col items-center gap-8 px-5 py-16">
          <div className="space-y-4 text-center">
            <h1 className="text-4xl font-bold tracking-tighter md:text-6xl">
              <span className="bg-gradient-to-b from-white to-white/75 bg-clip-text text-transparent">
                Create
              </span>{" "}
              <span className="bg-gradient-to-b from-blue-50 to-violet-500/90 bg-clip-text text-transparent">
                Nhollas
              </span>{" "}
              <span className="bg-gradient-to-b from-white to-white/75 bg-clip-text text-transparent">
                App
              </span>
            </h1>
            <p className="text-lg text-white/60 md:text-xl">
              A modern full-stack development template for Next.js applications.
            </p>
          </div>
        </div>
      </div>

      {/* Content Section */}
      <div className="relative">
        <div className="absolute inset-0 bg-[radial-gradient(ellipse_at_top,_var(--tw-gradient-stops))] from-blue-600/30 via-transparent to-transparent" />
        <div className="absolute inset-0 bg-gradient-to-b from-violet-600/10 via-transparent to-transparent" />
        <div className="relative mx-auto max-w-screen-sm px-5 py-16">
          <div className="rounded-xl border border-white/10 bg-white/5 backdrop-blur-sm">
            <div className="space-y-8 p-8">
              {/* Instructions */}
              <div className="space-y-4">
                <p className="flex items-baseline gap-3">
                  <span className="text-white/40">1.</span>
                  <span>
                    Get started by editing{" "}
                    <code className="rounded bg-white/10 px-2 py-1 font-mono">
                      app/page.tsx
                    </code>
                  </span>
                </p>
                <p className="flex items-baseline gap-3">
                  <span className="text-white/40">2.</span>
                  <span>Save and see your changes instantly.</span>
                </p>
              </div>

              {/* CTA */}
              <div className="flex justify-center gap-4">
                <Button
                  size="lg"
                  asChild
                  className="group relative h-12 w-full max-w-xs bg-white text-black hover:bg-white/90 focus:ring-2 focus:ring-blue-600/75 focus:ring-offset-2 focus:ring-offset-black focus-visible:ring-2 focus-visible:ring-blue-600/75 focus-visible:ring-offset-2 focus-visible:ring-offset-black"
                >
                  <Link
                    href="https://github.com/Nhollas/create-nhollas-app"
                    target="_blank"
                  >
                    <div className="absolute inset-0 bg-gradient-to-r from-blue-500/30 via-violet-500/30 to-transparent opacity-0 transition-opacity group-hover:opacity-100" />
                    <span className="relative flex items-center text-base">
                      <FileCode2 className="mr-2 size-6" />
                      Read the docs
                    </span>
                  </Link>
                </Button>
              </div>

              {/* Tech Stack */}
              <div className="border-t border-white/10 pt-8">
                <p className="mb-8 text-center text-white/60">
                  Leveraging industry-leading tools and technologies.
                </p>
                <div className="grid grid-cols-3 items-center justify-items-center gap-8 md:grid-cols-6">
                  <div className="relative size-9 opacity-80 transition-opacity hover:opacity-100">
                    <Image
                      src="/vitest-logo.svg"
                      alt="Vitest"
                      fill
                      className="text-white"
                    />
                  </div>
                  <div className="relative size-9 opacity-80 transition-opacity hover:opacity-100">
                    <Image
                      src="/playwright-logo.svg"
                      alt="Playwright"
                      fill
                      className="text-white"
                    />
                  </div>
                  <div className="relative size-9 opacity-80 transition-opacity hover:opacity-100">
                    <Image
                      src="/tailwindcss-logo.svg"
                      alt="Tailwind CSS"
                      fill
                      className="text-white"
                    />
                  </div>
                  <div className="relative size-9 opacity-80 transition-opacity hover:opacity-100">
                    <Image
                      src="/prettier-logo.svg"
                      alt="Prettier"
                      fill
                      className="text-white"
                    />
                  </div>
                  <div className="relative size-9 opacity-80 transition-opacity hover:opacity-100">
                    <Image
                      src="/tanstack-logo.png"
                      alt="Tanstack"
                      fill
                      className="text-white"
                    />
                  </div>
                  <div className="relative size-9 opacity-80 transition-opacity hover:opacity-100">
                    <Image
                      src="/zod-logo.svg"
                      alt="Zod"
                      fill
                      className="text-white"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
