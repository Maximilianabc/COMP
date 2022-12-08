function lerp(value1, value2, percentage) {
    if(percentage < 0) return value1;
    if(percentage > 1) return value2;
    return value1 + (value2 - value1) * percentage;
}

function lerpExpo(value1, value2, percentage, exponent, epsilon = 0.01) {
    var isAttack = value1 < value2;
    var value = Math.pow(isAttack ? percentage : 1 - percentage, exponent) * Math.abs(value1 - value2) + (isAttack ? value1 : value2);
    var truncate = Math.abs(value - value2) <= epsilon || (isAttack && value >= value2) || (!isAttack && value <= value2);
    return truncate ? value2 : value;
}