namespace Jobby.Client.Extensions;

public static class ActivityTypeExtension
{
    public static string ToCssClass(this int type) => _activityTypeCssClasses[type];

    static readonly Dictionary<int, string> _activityTypeCssClasses = new()
    {
        {1, "bg-red-50 text-red-500"},
        {2, "bg-orange-50 text-orange-500"},
        {3, "bg-amber-50 text-amber-500"},
        {4, "bg-yellow-50 text-yellow-500"},
        {5, "bg-lime-50 text-lime-500"},
        {6, "bg-green-50 text-green-500"},
        {7, "bg-emerald-50 text-emerald-500"},
        {8, "bg-teal-50 text-teal-500"},
        {9, "bg-cyan-50 text-cyan-500"},
        {10, "bg-sky-50 text-sky-500"},
        {11, "bg-blue-50 text-blue-500"},
        {12, "bg-indigo-50 text-indigo-500"},
        {13, "bg-violet-50 text-violet-500"},
        {14, "bg-purple-50 text-purple-500"},
        {15, "bg-fuchsia-50 text-fuchsia-500"},
        {16, "bg-pink-50 text-pink-500"},
        {17, "bg-rose-50 text-rose-500"},
        {18, "bg-red-50 text-red-500"},
        {19, "bg-orange-50 text-orange-500"},
        {20, "bg-amber-50 text-amber-500"},
        {21, "bg-yellow-50 text-yellow-500"},
        {22, "bg-lime-50 text-lime-500"},
        {23, "bg-green-50 text-green-500"}
    };
}
