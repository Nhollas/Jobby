import {
  Card,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { APIError } from "@/types";

export default function ApiErrorMessage({ error }: { error: APIError }) {
  return (
    <div className="p-6">
      <Card className="w-full max-w-xl bg-red-300">
        <CardHeader>
          <CardTitle>Error Occurred {error.status}</CardTitle>
          <CardDescription>{error.message}</CardDescription>
        </CardHeader>
      </Card>
    </div>
  );
}
