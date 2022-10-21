var $ = jQuery.noConflict();
var this_js_script = $('script[src*=accessibilitymenu]'); // or better regexp to get the file name..

var debugMode = false;

//var lang = this_js_script.attr('data-language');
//if (typeof lang === "undefined") {
//    var lang = 'heb';
//}

var lang = getScriptAttribute(this_js_script, "language", "eng");
var accessibilitySign = getScriptAttribute(this_js_script, "sign", "classic");
var sourceMenu = getScriptAttribute(this_js_script, "oemsource", "regular");

if (sourceMenu == "accessible+") {
    if (typeof curLang != "undefined") {
        if (debugMode) console.log(curLang);
        lang = curLang;
    } else {
        switch (lang) {
            case "heb":
                lang = "he_IL";
                break;
            case "eng":
                lang = "en_US";
        }
    }
} else {
    switch (lang) {
        case "heb":
            lang = "he_IL";
            break;
        case "eng":
            lang = "en_US";
    }
}

var accessibilityIconStr = (accessibilitySign === "classic") ? 'accessibility-menu/accessibility.png' : 'accessibility-menu/access.png';

if (debugMode) console.log("loaded with language: " + lang);

var accessibilityShown = false;
var textSizeBigger = false;
var areLinksYellow = false;
var isGrayscale = false;
var isHighContrast = false;
var areLinksUnderlined = false;


var delimiter = "|";
var cookieName = "accessibility";

var init = true;


prepMenu();

setAccessibityFromCookie();
makeAccessible();

function getScriptAttribute(scriptObject, attributeName, defaultValue) {
    var tmp = scriptObject.attr('data-' + attributeName);
    if (typeof tmp === "undefined") {
        var tmp = defaultValue;
    }
    return tmp;
}

function prepMenu() {
    console.log("############################prepMenu");

    var cssText = "<link href='accessibility-menu/yellow-links.css' title='yellow' type='text/css' rel='stylesheet' disabled='true'/>";
    cssText += "<link href='accessibility-menu/underline-links.css' title='underline' type='text/css' rel='stylesheet' disabled='true'/>";
    cssText += "<link href='accessibility-menu/grayscale.css' title='grayscale' type='text/css' rel='stylesheet' disabled='true'/>";

    if (sourceMenu == "accessible+") {
        cssText += "<link href='accessibility-menu/night.css' title='contrast' type='text/css' rel='stylesheet' disabled='true'/>";
        cssText += "<link href='accessibility-menu/biggest.css' title='textsize' type='text/css' rel='stylesheet' disabled='true'/>";
    } else {
        cssText += "<link href='accessibility-menu/high-contrast.css' title='contrast' type='text/css' rel='stylesheet' disabled='true'/>";
        cssText += "<link href='accessibility-menu/bigger.css' title='textsize' type='text/css' rel='stylesheet' disabled='true'/>";
    }





    $("head").append(cssText);



    $("nav.navPlacementStyle").append("<div id='slideout'></div>");
    var menuHTML = "";



    menuHTML += "<button class='transparentButton pull-right' onclick='toggleAccessibilityMenu()'><img class='accessibilityimage' src='" + accessibilityIconStr + "' alt='Accessible Menu Icon' role='navigation' title='Accessible Menu'/></button>";


    switch (lang) {

        case "en_US":
        default:

            menuHTML += "<div style='padding:20px;float:left;width:248px;background-color:#fff;border:1px solid #777;'><button style='width:206px;' class='accessibilityMenuButton alignLeft ltr' tabindex='0' id='btnTextSizeOn' onclick='setTextSize(true);'><i class='fa fa-text-height' aria-hidden='true'></i>&nbsp;&nbsp;large font</button>";
             menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnTextSizeOff' onclick='setTextSize(false);'><i class='fa fa-font' aria-hidden='true'></i>&nbsp;&nbsp;normal font </li>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnHighContrastOn' onclick='ToggleHighContrastMode(true);'><i class='fa fa-adjust' aria-hidden='true'></i>&nbsp;&nbsp;high contrast mode</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnHighContrastOff' onclick='ToggleHighContrastMode(false);'><i class='fa fa-adjust' aria-hidden='true'></i>&nbsp;&nbsp;regular contrast mode</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnYellowLinksOn'onclick='ToggleLinksYellow(true);'><i class='fa fa-pencil' aria-hidden='true'></i>&nbsp;&nbsp;yellow colored links</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnYellowLinksOff' onclick='ToggleLinksYellow(false);'><i class='fa fa-pencil' aria-hidden='true'></i>&nbsp;&nbsp;normal colored links</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnGrayscaleOn' onclick='ToggleGrayscaleMode(true);'><i class='fa fa-barcode' aria-hidden='true'></i>&nbsp;&nbsp;grayscale color mode</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnGrayscaleOff' onclick='ToggleGrayscaleMode(false);'><i class='fa fa-barcode' aria-hidden='true'></i>&nbsp;&nbsp;regular color mode</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnLinksOn' onclick='ToggleUnderlineLinks(true);'><i class='fa fa-underline' aria-hidden='true'></i>&nbsp;&nbsp;underline links</button>";
            menuHTML += "<button class='accessibilityMenuButton alignLeft' tabindex='0' id='btnLinksOff' onclick='ToggleUnderlineLinks(false);'><i class='fa fa-underline' aria-hidden='true'></i>&nbsp;&nbsp;default links</button></div>";

            break;
    

    }





    $("#slideout").html(menuHTML);

    //$("body").prepend("<div id='slideout'>" + menuHTML + "</div>");






}

function toggleAccessibilityMenu() {

    if (accessibilityShown) {
        $("#slideout").css("left", "-250px");

    } else {
        $("#slideout").css("left", "0");


    }

    accessibilityShown = !accessibilityShown;
}


function setTextSize(newSize) {
    if (debugMode) console.log("set size: " + newSize);
    if (newSize === "true" || newSize === true) {

        textSizeBigger = true;

        if ($('link[title=textsize]').length == 0) {
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: "bigger.css",
                title: "textsize"
            }).appendTo("head");
            $('link[title=textsize]')[0].disabled = false;
        } else {
            $('link[title=textsize]')[0].disabled = false;
        }



        $("#btnTextSizeOff").removeAttr( "disabled" );
        $("#btnTextSizeOff").attr("aria-disabled", false);
        $("#btnTextSizeOn").attr("disabled","disabled");
        $("#btnTextSizeOn").attr("aria-disabled", true);

    } else {

        if ($('link[title=textsize]').length > 0) {
            $('link[title=textsize]')[0].disabled = true;
        }


        textSizeBigger = false;

        $("#btnTextSizeOff").attr("disabled","disabled");
        $("#btnTextSizeOff").attr("aria-disabled", true);
        $("#btnTextSizeOn").removeAttr( "disabled" );
        $("#btnTextSizeOn").attr("aria-disabled", false);
    }

    var str = textSizeBigger + delimiter + areLinksYellow + delimiter + isGrayscale + delimiter + isHighContrast + delimiter + areLinksUnderlined + delimiter;
    if (!init) createCookie(cookieName, str);

}

function ToggleUnderlineLinks(mode) {
    if (!(mode === "true" || mode === true)) {
        if ($('link[title=underline]').length > 0) {
            $('link[title=underline]')[0].disabled = true;
        }

        areLinksUnderlined = false;
        $("#btnLinksOff").attr("disabled","disabled");
        $("#btnLinksOff").attr("aria-disabled", true);
        $("#btnLinksOn").removeAttr( "disabled" );
        $("#btnLinksOn").attr("aria-disabled", false);

    } else {
        if (debugMode) console.log("0");
        if ($('link[title=underline]').length == 0) {
            if (debugMode) console.log("1");
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: "underline-links.css",
                title: "underline"
            }).appendTo("head");
        } else {
            if (debugMode) console.log("2");
            $('link[title=underline]')[0].disabled = false;
        }
        areLinksUnderlined = true;
        $("#btnLinksOff").removeAttr( "disabled" );
        $("#btnLinksOff").attr("aria-disabled", false);
        $("#btnLinksOn").attr("disabled","disabled");
        $("#btnLinksOn").attr("aria-disabled", true);


    }

    var str = textSizeBigger + delimiter + areLinksYellow + delimiter + isGrayscale + delimiter + isHighContrast + delimiter + areLinksUnderlined + delimiter;
    if (!init) createCookie(cookieName, str);
}

function ToggleLinksYellow(mode) {
    if (!(mode === "true" || mode === true)) {
        if ($('link[title=yellow]').length > 0) {
            $('link[title=yellow]')[0].disabled = true;
        }

        areLinksYellow = false;
        $("#btnYellowLinksOff").attr("disabled","disabled");
        $("#btnYellowLinksOff").attr("aria-disabled", true);
        $("#btnYellowLinksOn").removeAttr( "disabled" );
        $("#btnYellowLinksOn").attr("aria-disabled", false);

    } else {
        if ($('link[title=yellow]').length == 0) {
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: "yellow-links.css",
                title: "yellow"
            }).appendTo("head");
        } else {
            $('link[title=yellow]')[0].disabled = false;
        }
        areLinksYellow = true;

        $("#btnYellowLinksOff").removeAttr( "disabled" );
        $("#btnYellowLinksOff").attr("aria-disabled", false);
        $("#btnYellowLinksOn").attr("disabled","disabled");
        $("#btnYellowLinksOn").attr("aria-disabled", true);
    }

    var str = textSizeBigger + delimiter + areLinksYellow + delimiter + isGrayscale + delimiter + isHighContrast + delimiter + areLinksUnderlined + delimiter;
    if (!init) createCookie(cookieName, str);
}

function ToggleHighContrastMode(mode) {
    if (!(mode === "true" || mode === true)) {
        if ($('link[title=contrast]').length > 0) {
            $('link[title=contrast]')[0].disabled = true;
        }
        isHighContrast = false;
        $("#btnHighContrastOff").attr("disabled","disabled");
        $("#btnHighContrastOff").attr("aria-disabled", true);
        $("#btnHighContrastOn").removeAttr( "disabled" );
        $("#btnHighContrastOn").attr("aria-disabled", false);
    } else {
        if ($('link[title=contrast]').length == 0) {
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: "high-contrast.css",
                title: "contrast"
            }).appendTo("head");
            $('link[title=contrast]')[0].disabled = false;
        } else {
            $('link[title=contrast]')[0].disabled = false;
        }
        isHighContrast = true;
        $("#btnHighContrastOff").removeAttr( "disabled" );
        $("#btnHighContrastOff").attr("aria-disabled", false);
        $("#btnHighContrastOn").attr("disabled","disabled");
        $("#btnHighContrastOn").attr("aria-disabled", true);
    }
    var str = textSizeBigger + delimiter + areLinksYellow + delimiter + isGrayscale + delimiter + isHighContrast + delimiter + areLinksUnderlined + delimiter;
    if (!init) createCookie(cookieName, str);
}

function ToggleGrayscaleMode(mode) {
    if (!(mode === "true" || mode === true)) {
        if ($('link[title=grayscale]').length > 0) {
            $('link[title=grayscale]')[0].disabled = true;
        }
        isGrayscale = false;
        $("#btnGrayscaleOff").attr("disabled","disabled");
        $("#btnGrayscaleOff").attr("aria-disabled", true);
        $("#btnGrayscaleOn").removeAttr( "disabled" );
        $("#btnGrayscaleOn").attr("aria-disabled", false);
    } else {
        if ($('link[title=contrast]').length == 0) {
            $("<link/>", {
                rel: "stylesheet",
                type: "text/css",
                href: "grayscale.css",
                title: "grayscale"
            }).appendTo("head");
            $('link[title=grayscale]')[0].disabled = false;
        } else {
            $('link[title=grayscale]')[0].disabled = false;
        }
        isGrayscale = true;
        $("#btnGrayscaleOff").removeAttr( "disabled" );
        $("#btnGrayscaleOff").attr("aria-disabled", false);
        $("#btnGrayscaleOn").attr("disabled","disabled");
        $("#btnGrayscaleOn").attr("aria-disabled", true);
    }



    var str = textSizeBigger + delimiter + areLinksYellow + delimiter + isGrayscale + delimiter + isHighContrast + delimiter + areLinksUnderlined + delimiter;
    if (!init) createCookie(cookieName, str);
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "" + date.toGMTString();
    } else {
        var date = new Date();
        date.setTime(date.getTime() + (30 * 24 * 60 * 60 * 1000));
        var expires = "" + date.toGMTString();
    }
    document.cookie = name + "=" + value + expires + ";SameSite=Lax; path=/";
    if (debugMode) console.log("create cookie: " + value);

}



function readCookie(name) {
    if (debugMode) console.log("trying to get cookie: " + name);
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) {
            if (debugMode) console.log("cookie: " + name + " found!");
            c = c.substring(nameEQ.length, c.length);
            //alert("in read cookie. got: " + c);
            var parts = c.split(delimiter);
            if (parts.length > 4) {
                areLinksUnderlined = parts[3];
                if (debugMode) console.log("underlined: " + areLinksUnderlined);
            }
            if (parts.length > 3) {
                isHighContrast = parts[3];
                if (debugMode) console.log("contrast: " + isHighContrast);
            }
            if (parts.length > 2) {
                isGrayscale = parts[2];
                if (debugMode) console.log("grayscale: " + isGrayscale);
            }
            if (parts.length > 1) {
                areLinksYellow = parts[1];
                if (debugMode) console.log("yellow: " + areLinksYellow);
            }
            if (parts.length > 0) {
                //alert("read from cookie:" + parts[0]);
                textSizeBigger = parts[0];
                if (debugMode) console.log("size: " + textSizeBigger);
            }

        }
    }
    return null;
}

function setAccessibityFromCookie() {
    readCookie(cookieName);
    setTextSize(textSizeBigger);
    ToggleGrayscaleMode(isGrayscale);
    ToggleHighContrastMode(isHighContrast);
    ToggleLinksYellow(areLinksYellow);
    ToggleUnderlineLinks(areLinksUnderlined);

    init = false;
}

function makeAccessible() {

    // set empty alt attribute to any image that does not contain an alt tag:
    $('img:not([alt])').attr('alt', '');

    // copy link title into aria-label:
    $('a[title]').each(function () {
        $(this).attr('aria-label', $(this).attr('title'));
    });

    if ($("nav").attr('role') == undefined) {
        $("nav").attr('role', "navigation");
    }

}
