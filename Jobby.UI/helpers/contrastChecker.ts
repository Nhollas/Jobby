export function contrastChecker(bgColor: string, textColor: string): string {
  const bgColorRGB = hexToRgb(bgColor);
  const textColorRGB = hexToRgb(textColor);
  const bgColorYIQ = (bgColorRGB.r * 299 + bgColorRGB.g * 587 + bgColorRGB.b * 114) / 1000;
  const textColorYIQ = (textColorRGB.r * 299 + textColorRGB.g * 587 + textColorRGB.b * 114) / 1000;
  const contrast = (Math.max(bgColorYIQ, textColorYIQ) + 0.05) / (Math.min(bgColorYIQ, textColorYIQ) + 0.05);
  return contrast >= 4.5 ? 'dark' : 'light';
}

// Path: helpers\hexToRgb.ts
export function hexToRgb(hex: string): { r: number; g: number; b: number } {
  const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
  return result
    ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16),
      }
    : { r: 0, g: 0, b: 0 };
}
