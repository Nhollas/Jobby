function ClassNames(...args) {
  return args
    .filter((arg) => arg)
    .map((arg) => {
      if (typeof arg === "string") {
        return arg;
      } else if (typeof arg === "object") {
        return Object.keys(arg)
          .map((key) => {
            const value = arg[key];
            if (typeof value === "boolean") {
              return value ? key : "";
            } else if (typeof value === "string") {
              return `${key}-${value}`;
            } else if (typeof value === "object") {
              const { default: defaultValue, ...variants } = value;
              const variant = Object.keys(variants).find(
                (variant) => variants[variant]
              );
              return variant ? `${key}-${variant}` : defaultValue;
            }
          })
          .join(" ");
      }
    })
    .join(" ");
}

export default ClassNames;
