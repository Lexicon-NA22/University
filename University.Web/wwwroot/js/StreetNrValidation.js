
$.validator.addMethod('streetnr', function (value, element, params) {
    let nr = value.split(' ').pop();
    let res = parseInt(nr);
    if (isNaN(res)) return false;
    let max = parseInt(params);
    if (isNaN(max)) return false;
    return res <= max
});

$.validator.unobtrusive.adapters.addSingleVal('streetnr', 'max');