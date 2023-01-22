export function contrastChecker(bgColor: string, textColor: string): string {
  const threshold = 4.5;
  const contrastRatio = getContrastRatio(bgColor, textColor);
  if (contrastRatio < threshold) {
    return invertColor(textColor);
  } else {
    return textColor;
  }
}

function getContrastRatio(bgColor: string, textColor: string): number {
  const bgLuminance = getLuminance(bgColor);

  const textLuminance = getLuminance(textColor);

  return (Math.max(bgLuminance, textLuminance) + 0.05) / (Math.min(bgLuminance, textLuminance) + 0.05);
}

function getLuminance(color: string): number {
  const colorRGB = color.match(/\d+/g);

  console.log(colorRGB)
  if(!colorRGB) throw new Error('Invalid color format. Provide hex color code');
  for (let i = 0; i < 3; i++) {
    colorRGB[i] = parseInt(colorRGB[i], 16);
    colorRGB[i] /= 255;
    colorRGB[i] = colorRGB[i] <= 0.03928 ? colorRGB[i] / 12.92 : Math.pow((colorRGB[i] + 0.055) / 1.055, 2.4);
  }
  return 0.2126 * colorRGB[0] + 0.7152 * colorRGB[1] + 0.0722 * colorRGB[2];
}

function invertColor(hex: string): string {
    if (hex.indexOf('#') === 0) {
        hex = hex.slice(1);
    }
    // convert 3-digit hex to 6-digits.
    if (hex.length === 3) {
        hex = hex[0] + hex[0] + hex[1] + hex[1] + hex[2] + hex[2];
    }
    if (hex.length !== 6) {
        throw new Error('Invalid HEX color.');
    }
    var r = (255 - parseInt(hex.slice(0, 2), 16)).toString(16),
        g = (255 - parseInt(hex.slice(2, 4), 16)).toString(16),
        b = (255 - parseInt(hex.slice(4, 6), 16)).toString(16);
    // pad each with zeros and return
    return '#' + padZero(r) + padZero(g) + padZero(b);
}

function padZero(str: string): string {
    if (str.length === 1) {
        return '0' + str;
    }
    return str;
}
