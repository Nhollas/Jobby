namespace Jobby.Client.Extensions;

public static class ActivityTypeExtension
{
    static readonly Dictionary<int, string> _activityTypeCssClasses = new()
    {
        {1, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-red-400"},
        {2, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-red-600"},
        {3, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-orange-400"},
        {4, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-orange-600"},
        {5, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-amber-400"},
        {6, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-amber-400"},
        {7, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-lime-400"},
        {8, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-lime-600"},
        {9, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-emerald-400"},
        {10, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-emerald-600"},
        {11, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-teal-400"},
        {12, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-teal-600"},
        {13, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-blue-400"},
        {14, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-blue-600"},
        {15, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-violet-400"},
        {16, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-violet-600"},
        {17, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-purple-400"},
        {18, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-purple-600"},
        {19, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-fuchsia-400"},
        {20, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-fuchsia-600"},
        {21, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-pink-400"},
        {22, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-pink-600"},
        {23, "flex justify-center items-center whitespace-nowrap text-white text-xs rounded-md px-3 py-0.5 font-semibold bg-rose-600"}
    };

    public static string ToCssClass(this int type) => _activityTypeCssClasses[type];
}
