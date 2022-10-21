/*
Copyright © 2013 Adobe Systems Incorporated.

Licensed under the Apache License, Version 2.0 (the “License”);
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an “AS IS” BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/**
 * See <a href="http://jquery.com">http://jquery.com</a>.
 * @name jquery
 * @class
 * See the jQuery Library  (<a href="http://jquery.com">http://jquery.com</a>) for full details.  This just
 * documents the function and classes that are added to jQuery by this plug-in.
 */

/**
 * See <a href="http://jquery.com">http://jquery.com</a>
 * @name fn
 * @class
 * See the jQuery Library  (<a href="http://jquery.com">http://jquery.com</a>) for full details.  This just
 * documents the function and classes that are added to jQuery by this plug-in.
 * @memberOf jquery
 */

/**
 * @fileOverview accessibleMegaMenu plugin
 *
 *<p>Licensed under the Apache License, Version 2.0 (the “License”)
 *<br />Copyright © 2013 Adobe Systems Incorporated.
 *<br />Project page <a href="https://github.com/adobe-accessibility/Accessible-Mega-Menu">https://github.com/adobe-accessibility/Accessible-Mega-Menu</a>
 * @version 0.1
 * @author Michael Jordan
 * @requires jquery
 */

/*jslint browser: true, devel: true, plusplus: true, nomen: true */
/*global jQuery, window, document */
(function ($, window, document) {
    "use strict";
    var pluginName = "accessibleMegaMenu",
        defaults = {
            uuidPrefix: "accessible-megamenu", // unique ID's are required to indicate aria-owns, aria-controls and aria-labelledby
            menuClass: "accessible-megamenu", // default css class used to define the megamenu styling
            topNavItemClass: "accessible-megamenu-top-nav-item", // default css class for a top-level navigation item in the megamenu
            panelClass: "accessible-megamenu-panel", // default css class for a megamenu panel
            panelGroupClass: "accessible-megamenu-panel-group", // default css class for a group of items within a megamenu panel
            hoverClass: "hover", // default css class for the hover state
            focusClass: "focus", // default css class for the focus state
            openClass: "open", // default css class for the open state,
            toggleButtonClass: "accessible-megamenu-toggle", // default css class responsive toggle button
            openDelay: 0, // default open delay when opening menu via mouseover
            closeDelay: 250, // default open delay when opening menu via mouseover
            openOnMouseover: false // default setting for whether menu should open on mouseover
        },
        Keyboard = {
            BACKSPACE: 8,
            COMMA: 188,
            DELETE: 46,
            DOWN: 40,
            END: 35,
            ENTER: 13,
            ESCAPE: 27,
            HOME: 36,
            LEFT: 37,
            PAGE_DOWN: 34,
            PAGE_UP: 33,
            PERIOD: 190,
            RIGHT: 39,
            SPACE: 32,
            TAB: 9,
            UP: 38,
            keyMap: {
                48: "0",
                49: "1",
                50: "2",
                51: "3",
                52: "4",
                53: "5",
                54: "6",
                55: "7",
                56: "8",
                57: "9",
                59: ";",
                65: "a",
                66: "b",
                67: "c",
                68: "d",
                69: "e",
                70: "f",
                71: "g",
                72: "h",
                73: "i",
                74: "j",
                75: "k",
                76: "l",
                77: "m",
                78: "n",
                79: "o",
                80: "p",
                81: "q",
                82: "r",
                83: "s",
                84: "t",
                85: "u",
                86: "v",
                87: "w",
                88: "x",
                89: "y",
                90: "z",
                96: "0",
                97: "1",
                98: "2",
                99: "3",
                100: "4",
                101: "5",
                102: "6",
                103: "7",
                104: "8",
                105: "9",
                190: "."
            }
        },
        clearTimeout = window.clearTimeout,
        setTimeout = window.setTimeout,
        isOpera = window.opera && window.opera.toString() === '[object Opera]';
    /**
     * @desc Creates a new accessible mega menu instance.
     * @param {jquery} element
     * @param {object} [options] Mega Menu options
     * @param {string} [options.uuidPrefix=accessible-megamenu] - Prefix for generated unique id attributes, which are required to indicate aria-owns, aria-controls and aria-labelledby
     * @param {string} [options.menuClass=accessible-megamenu] - CSS class used to define the megamenu styling
     * @param {string} [options.topNavItemClass=accessible-megamenu-top-nav-item] - CSS class for a top-level navigation item in the megamenu
     * @param {string} [options.panelClass=accessible-megamenu-panel] - CSS class for a megamenu panel
     * @param {string} [options.panelGroupClass=accessible-megamenu-panel-group] - CSS class for a group of items within a megamenu panel
     * @param {string} [options.hoverClass=hover] - CSS class for the hover state
     * @param {string} [options.focusClass=focus] - CSS class for the focus state
     * @param {string} [options.openClass=open] - CSS class for the open state
     * @constructor
     */
    function AccessibleMegaMenu(element, options) {
        this.element = element;

        // merge optional settings and defaults into settings
        this.settings = $.extend({}, defaults, options);

        this._defaults = defaults;
        this._name = pluginName;

        this.mouseTimeoutID = null;
        this.focusTimeoutID = null;
        this.mouseFocused = false;
        this.justFocused = false;

        this.init();

        var isMobile = false; //initiate as false
        // device detection
        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
            isMobile = true;
        }

        if (isMobile) {
            var myMenuClass = $(".nav-menu");
            myMenuClass.hide();
            myMenuClass.attr("aria-expanded", "false");
        } else {
            var myMenuClass = $(".nav-menu");

            myMenuClass.attr("aria-expanded", "true");
        }
    }

    AccessibleMegaMenu.prototype = (function () {

        /* private attributes and methods ------------------------ */
        var uuid = 0,
            keydownTimeoutDuration = 1000,
            keydownSearchString = "",
            isTouch = 'ontouchstart' in window || window.navigator.msMaxTouchPoints,
            _getPlugin,
            _addUniqueId,
            _togglePanel,
            _clickHandler,
            _touchmoveHandler,
            _clickOutsideHandler,
            _DOMAttrModifiedHandler,
            _focusInHandler,
            _focusOutHandler,
            _keyDownHandler,
            _mouseDownHandler,
            _mouseOverHandler,
            _mouseOutHandler,
            _clickToggleHandler,
            _toggleExpandedEventHandlers,
            _addEventHandlers,
            _removeEventHandlers;

        /**
         * @name jQuery.fn.accessibleMegaMenu~_getPlugin
         * @desc Returns the parent accessibleMegaMenu instance for a given element
         * @param {jQuery} element
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _getPlugin = function (element) {
            return $(element).closest(':data(plugin_' + pluginName + ')').data("plugin_" + pluginName);
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_addUniqueId
         * @desc Adds a unique id and element.
         * The id string starts with the
         * string defined in settings.uuidPrefix.
         * @param {jQuery} element
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _addUniqueId = function (element) {
            element = $(element);
            var settings = this.settings;
            if (!element.attr("id")) {
                element.attr("id", settings.uuidPrefix + "-" + new Date().getTime() + "-" + (++uuid));
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_togglePanel
         * @desc Toggle the display of mega menu panels in response to an event.
         * The optional boolean value 'hide' forces all panels to hide.
         * @param {event} event
         * @param {Boolean} [hide] Hide all mega menu panels when true
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _togglePanel = function (event, hide) {
            var target = $(event.target),
                that = this,
                settings = this.settings,
                menu = this.menu,
                topli = target.closest('.' + settings.topNavItemClass),
                panel = target.hasClass(settings.panelClass) ? target : target.closest('.' + settings.panelClass),
                newfocus;

            _toggleExpandedEventHandlers.call(this, true);

            if (hide) {
                topli = menu.find('.' + settings.topNavItemClass + ' .' + settings.openClass + ':first').closest('.' + settings.topNavItemClass);
                if (!(topli.is(event.relatedTarget) || topli.has(event.relatedTarget).length > 0)) {
                    if ((event.type === 'mouseout' || event.type === 'focusout') && topli.has(document.activeElement).length > 0) {
                        return;
                    }
                    topli.find('[aria-expanded]')
                        .attr('aria-expanded', 'false')
                        .removeClass(settings.openClass)
                        .filter('.' + settings.panelClass)
                        .attr('aria-hidden', 'true');
                    if ((event.type === 'keydown' && event.keyCode === Keyboard.ESCAPE) || event.type === 'DOMAttrModified') {
                        newfocus = topli.find(':tabbable:first');
                        setTimeout(function () {
                            menu.find('[aria-expanded].' + that.settings.panelClass).off('DOMAttrModified.accessible-megamenu');
                            newfocus.focus();
                            that.justFocused = false;
                        }, 99);
                    }
                } else if (topli.length === 0) {
                    menu.find('[aria-expanded=true]')
                        .attr('aria-expanded', 'false')
                        .removeClass(settings.openClass)
                        .filter('.' + settings.panelClass)
                        .attr('aria-hidden', 'true');
                }
            } else {
                clearTimeout(that.focusTimeoutID);
                topli.siblings()
                    .find('[aria-expanded]')
                    .attr('aria-expanded', 'false')
                    .removeClass(settings.openClass)
                    .filter('.' + settings.panelClass)
                    .attr('aria-hidden', 'true');
                topli.find('[aria-expanded]')
                    .attr('aria-expanded', 'true')
                    .addClass(settings.openClass)
                    .filter('.' + settings.panelClass)
                    .attr('aria-hidden', 'false');

                var pageScrollPosition = $('html')[0].scrollTop;
                var openPanelTopPosition = $('.' + settings.panelClass + '.' + settings.openClass).parent().offset().top;

                if (pageScrollPosition > openPanelTopPosition) {
                    $('html')[0].scrollTop = openPanelTopPosition;
                }

                if (event.type === 'mouseover' && target.is(':tabbable') && topli.length === 1 && panel.length === 0 && menu.has(document.activeElement).length > 0) {
                    target.focus();
                    that.justFocused = false;
                }

                _toggleExpandedEventHandlers.call(that);
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_clickHandler
         * @desc Handle click event on mega menu item
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _clickHandler = function (event) {
            var target = $(event.target).closest(':tabbable'),
                topli = target.closest('.' + this.settings.topNavItemClass),
                panel = target.closest('.' + this.settings.panelClass);
            if (topli.length === 1 && panel.length === 0 && topli.find('.' + this.settings.panelClass).length === 1) {
                if (!target.hasClass(this.settings.openClass)) {
                    event.preventDefault();
                    event.stopPropagation();
                    _togglePanel.call(this, event);
                    this.justFocused = false;
                } else {
                    if (this.justFocused) {
                        event.preventDefault();
                        event.stopPropagation();
                        this.justFocused = false;
                    } else if (isTouch || !isTouch && !this.settings.openOnMouseover) {
                        event.preventDefault();
                        event.stopPropagation();
                        _togglePanel.call(this, event, target.hasClass(this.settings.openClass));
                    }
                }
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_touchmoveHandler
         * @desc Handle touch move event on menu
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _touchmoveHandler = function () {
            this.justMoved = true;
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_clickOutsideHandler
         * @desc Handle click event outside of a the megamenu
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _clickOutsideHandler = function (event) {
            if ($(event.target).closest(this.menu).length === 0) {
                event.preventDefault();
                event.stopPropagation();
                _togglePanel.call(this, event, true);
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_DOMAttrModifiedHandler
         * @desc Handle DOMAttrModified event on panel to respond to Windows 8 Narrator ExpandCollapse pattern
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _DOMAttrModifiedHandler = function (event) {
            if (event.originalEvent.attrName === 'aria-expanded' && event.originalEvent.newValue === 'false' && $(event.target).hasClass(this.settings.openClass)) {
                event.preventDefault();
                event.stopPropagation();
                _togglePanel.call(this, event, true);
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_focusInHandler
         * @desc Handle focusin event on mega menu item.
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _focusInHandler = function (event) {
            clearTimeout(this.focusTimeoutID);
            var target = $(event.target),
                panel = target.closest('.' + this.settings.panelClass);
            target
                .addClass(this.settings.focusClass);
            this.justFocused = !this.mouseFocused || (!this.settings.openOnMouseover && this.mouseFocused);
            this.mouseFocused = false;
            if (this.justFocused && this.panels.not(panel).filter('.' + this.settings.openClass).length) {
                _togglePanel.call(this, event);
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_focusOutHandler
         * @desc Handle focusout event on mega menu item.
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _focusOutHandler = function (event) {
            this.justFocused = false;
            var that = this,
                target = $(event.target),
                topli = target.closest('.' + this.settings.topNavItemClass);
            target
                .removeClass(this.settings.focusClass);

            if (window.cvox) {
                // If ChromeVox is running...
                that.focusTimeoutID = setTimeout(function () {
                    window.cvox.Api.getCurrentNode(function (node) {
                        if (topli.has(node).length) {
                            // and the current node being voiced is in
                            // the mega menu, clearTimeout,
                            // so the panel stays open.
                            clearTimeout(that.focusTimeoutID);
                        } else {
                            that.focusTimeoutID = setTimeout(function (scope, event, hide) {
                                _togglePanel.call(scope, event, hide);
                            }, 275, that, event, true);
                        }
                    });
                }, 25);
            } else {
                that.focusTimeoutID = setTimeout(function () {
                    if (that.mouseFocused && event.relatedTarget === null) {
                        return;
                    }
                    _togglePanel.call(that, event, true);
                }, 300);
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_keyDownHandler
         * @desc Handle keydown event on mega menu.
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _keyDownHandler = function (event) {
            var that = (this.constructor === AccessibleMegaMenu) ? this : _getPlugin(this), // determine the AccessibleMegaMenu plugin instance
                settings = that.settings,
                target = $($(this).is('.' + settings.hoverClass + ':tabbable') ? this : event.target), // if the element is hovered the target is this, otherwise, its the focused element
                menu = that.menu,
                topnavitems = that.topnavitems,
                topli = target.closest('.' + settings.topNavItemClass),
                tabbables = menu.find(':tabbable'),
                panel = target.hasClass(settings.panelClass) ? target : target.closest('.' + settings.panelClass),
                panelGroups = panel.find('.' + settings.panelGroupClass),
                currentPanelGroup = target.closest('.' + settings.panelGroupClass),
                next,
                keycode = event.keyCode || event.which,
                start,
                i,
                o,
                label,
                found = false,
                newString = Keyboard.keyMap[event.keyCode] || '',
                regex,
                isTopNavItem = (topli.length === 1 && panel.length === 0);

            if (target.is("input:focus, select:focus, textarea:focus, button:focus")) {
                // if the event target is a form element we should handle keydown normally
                return;
            }

            if (target.is('.' + settings.hoverClass + ':tabbable')) {
                $('html').off('keydown.accessible-megamenu');
            }

            switch (keycode) {
                case Keyboard.ESCAPE:
                    this.mouseFocused = false;
                    _togglePanel.call(that, event, true);
                    break;
                case Keyboard.DOWN:
                    event.preventDefault();
                    this.mouseFocused = false;
                    if (isTopNavItem) {
                        _togglePanel.call(that, event);
                        found = (topli.find('.' + settings.panelClass + ' :tabbable:first').focus().length === 1);
                    } else {
                        found = (tabbables.filter(':gt(' + tabbables.index(target) + '):first').focus().length === 1);
                    }

                    if (!found && isOpera && (event.ctrlKey || event.metaKey)) {
                        tabbables = $(':tabbable');
                        i = tabbables.index(target);
                        found = ($(':tabbable:gt(' + $(':tabbable').index(target) + '):first').focus().length === 1);
                    }
                    break;
                case Keyboard.UP:
                    event.preventDefault();
                    this.mouseFocused = false;
                    if (isTopNavItem && target.hasClass(settings.openClass)) {
                        _togglePanel.call(that, event, true);
                        next = topnavitems.filter(':lt(' + topnavitems.index(topli) + '):last');
                        if (next.children('.' + settings.panelClass).length) {
                            found = (next.find('[aria-expanded]')
                                .attr('aria-expanded', 'true')
                                .addClass(settings.openClass)
                                .filter('.' + settings.panelClass)
                                .attr('aria-hidden', 'false')
                                .find(':tabbable:last')
                                .focus() === 1);
                        }
                    } else if (!isTopNavItem) {
                        found = (tabbables.filter(':lt(' + tabbables.index(target) + '):last').focus().length === 1);
                    }

                    if (!found && isOpera && (event.ctrlKey || event.metaKey)) {
                        tabbables = $(':tabbable');
                        i = tabbables.index(target);
                        found = ($(':tabbable:lt(' + $(':tabbable').index(target) + '):first').focus().length === 1);
                    }
                    break;
                case Keyboard.RIGHT:
                    event.preventDefault();
                    this.mouseFocused = false;
                    if (isTopNavItem) {
                        found = (topnavitems.filter(':gt(' + topnavitems.index(topli) + '):first').find(':tabbable:first').focus().length === 1);
                    } else {
                        if (panelGroups.length && currentPanelGroup.length) {
                            // if the current panel contains panel groups, and we are able to focus the first tabbable element of the next panel group
                            found = (panelGroups.filter(':gt(' + panelGroups.index(currentPanelGroup) + '):first').find(':tabbable:first').focus().length === 1);
                        }

                        if (!found) {
                            found = (topli.find(':tabbable:first').focus().length === 1);
                        }
                    }
                    break;
                case Keyboard.LEFT:
                    event.preventDefault();
                    this.mouseFocused = false;
                    if (isTopNavItem) {
                        found = (topnavitems.filter(':lt(' + topnavitems.index(topli) + '):last').find(':tabbable:first').focus().length === 1);
                    } else {
                        if (panelGroups.length && currentPanelGroup.length) {
                            // if the current panel contains panel groups, and we are able to focus the first tabbable element of the previous panel group
                            found = (panelGroups.filter(':lt(' + panelGroups.index(currentPanelGroup) + '):last').find(':tabbable:first').focus().length === 1);
                        }

                        if (!found) {
                            found = (topli.find(':tabbable:first').focus().length === 1);
                        }
                    }
                    break;
                case Keyboard.TAB:
                    this.mouseFocused = false;
                    i = tabbables.index(target);
                    if (event.shiftKey && isTopNavItem && target.hasClass(settings.openClass)) {
                        _togglePanel.call(that, event, true);
                        next = topnavitems.filter(':lt(' + topnavitems.index(topli) + '):last');
                        if (next.children('.' + settings.panelClass).length) {
                            found = next.children()
                                .attr('aria-expanded', 'true')
                                .addClass(settings.openClass)
                                .filter('.' + settings.panelClass)
                                .attr('aria-hidden', 'false')
                                .find(':tabbable:last')
                                .focus();
                        }
                    } else if (event.shiftKey && i > 0) {
                        found = (tabbables.filter(':lt(' + i + '):last').focus().length === 1);
                    } else if (!event.shiftKey && i < tabbables.length - 1) {
                        found = (tabbables.filter(':gt(' + i + '):first').focus().length === 1);
                    } else if (isOpera) {
                        tabbables = $(':tabbable');
                        i = tabbables.index(target);
                        if (event.shiftKey) {
                            found = ($(':tabbable:lt(' + $(':tabbable').index(target) + '):last').focus().length === 1);
                        } else {
                            found = ($(':tabbable:gt(' + $(':tabbable').index(target) + '):first').focus().length === 1);
                        }
                    }

                    if (found) {
                        event.preventDefault();
                    }
                    break;
                case Keyboard.SPACE:
                case Keyboard.ENTER:
                    if (isTopNavItem) {
                        event.preventDefault();
                        _clickHandler.call(that, event);
                    } else {
                        return true;
                    }
                    break;
                default:
                    // alphanumeric filter
                    clearTimeout(this.keydownTimeoutID);

                    keydownSearchString += newString !== keydownSearchString ? newString : '';

                    if (keydownSearchString.length === 0) {
                        return;
                    }

                    this.keydownTimeoutID = setTimeout(function () {
                        keydownSearchString = '';
                    }, keydownTimeoutDuration);

                    if (isTopNavItem && !target.hasClass(settings.openClass)) {
                        tabbables = tabbables.filter(':not(.' + settings.panelClass + ' :tabbable)');
                    } else {
                        tabbables = topli.find(':tabbable');
                    }

                    if (event.shiftKey) {
                        tabbables = $(tabbables.get()
                            .reverse());
                    }

                    for (i = 0; i < tabbables.length; i++) {
                        o = tabbables.eq(i);
                        if (o.is(target)) {
                            start = (keydownSearchString.length === 1) ? i + 1 : i;
                            break;
                        }
                    }

                    regex = new RegExp('^' + keydownSearchString.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&'), 'i');

                    for (i = start; i < tabbables.length; i++) {
                        o = tabbables.eq(i);
                        label = $.trim(o.text());
                        if (regex.test(label)) {
                            found = true;
                            o.focus();
                            break;
                        }
                    }
                    if (!found) {
                        for (i = 0; i < start; i++) {
                            o = tabbables.eq(i);
                            label = $.trim(o.text());
                            if (regex.test(label)) {
                                o.focus();
                                break;
                            }
                        }
                    }
                    break;
            }
            that.justFocused = false;
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_mouseDownHandler
         * @desc Handle mousedown event on mega menu.
         * @param {event} Event object
         * @memberof accessibleMegaMenu
         * @inner
         * @private
         */
        _mouseDownHandler = function (event) {
            if ($(event.target).closest(this.settings.panelClass) || $(event.target).closest(":focusable").length) {
                this.mouseFocused = true;
                if ($(event.target).closest(this.settings.menuClass)) {
                    $('html').on('keydown.accessible-megamenu', $.proxy(_keyDownHandler, event.target));
                }
            }
            clearTimeout(this.mouseTimeoutID);
            this.mouseTimeoutID = setTimeout(function () {
                clearTimeout(this.focusTimeoutID);
            }, 1);
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_mouseOverHandler
         * @desc Handle mouseover event on mega menu.
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _mouseOverHandler = function (event) {
            clearTimeout(this.mouseTimeoutID);
            var that = this;

            if (!that.settings.openOnMouseover) {
                return;
            }

            this.mouseTimeoutID = setTimeout(function () {
                $(event.target).addClass(that.settings.hoverClass);
                _togglePanel.call(that, event);
                if ($(event.target).closest(that.settings.menuClass)) {
                    $('html').on('keydown.accessible-megamenu', $.proxy(_keyDownHandler, event.target));
                }
            }, this.settings.openDelay);
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_mouseOutHandler
         * @desc Handle mouseout event on mega menu.
         * @param {event} Event object
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _mouseOutHandler = function (event) {
            clearTimeout(this.mouseTimeoutID);
            var that = this;

            if (!that.settings.openOnMouseover) {
                return;
            }

            $(event.target)
                .removeClass(that.settings.hoverClass);

            that.mouseTimeoutID = setTimeout(function () {
                _togglePanel.call(that, event, true);
            }, this.settings.closeDelay);
            if ($(event.target).is(':tabbable')) {
                $('html').off('keydown.accessible-megamenu');
            }
        };

        /**
         * @name jQuery.fn.accessibleMegaMenu~_clickToggleHandler
         * @desc Handle click event on menu toggle button.
         * @memberof jQuery.fn.accessibleMegaMenu
         * @inner
         * @private
         */
        _clickToggleHandler = function () {
            var isExpanded = this.toggleButton.attr('aria-expanded') === 'true';
            this.toggleButton.attr({
                'aria-expanded': !isExpanded,
                'aria-pressed': !isExpanded
            });
            var myMenuClass = $(".nav-menu");
            !(isExpanded) ? myMenuClass.show(): myMenuClass.hide();
            !(isExpanded) ? myMenuClass.attr("aria-expanded", "true"): myMenuClass.attr("aria-expanded", "false");

        };

        _toggleExpandedEventHandlers = function (hide) {
            var menu = this.menu;
            if (hide) {
                $('html').off('mouseup.outside-accessible-megamenu, touchend.outside-accessible-megamenu, mspointerup.outside-accessible-megamenu,  pointerup.outside-accessible-megamenu');

                menu.find('[aria-expanded].' + this.settings.panelClass).off('DOMAttrModified.accessible-megamenu');
            } else {
                $('html').on('mouseup.outside-accessible-megamenu, touchend.outside-accessible-megamenu, mspointerup.outside-accessible-megamenu,  pointerup.outside-accessible-megamenu', $.proxy(_clickOutsideHandler, this));

                /* Narrator in Windows 8 automatically toggles the aria-expanded property on double tap or click.
                   To respond to the change to collapse the panel, we must add a listener for a DOMAttrModified event. */
                menu.find('[aria-expanded=true].' + this.settings.panelClass).on('DOMAttrModified.accessible-megamenu', $.proxy(_DOMAttrModifiedHandler, this));
            }
        };

        _addEventHandlers = function () {
            var menu = this.menu,
                toggleButton = this.toggleButton;
            menu.on("focusin.accessible-megamenu", ":focusable, ." + this.settings.panelClass, $.proxy(_focusInHandler, this))
                .on("focusout.accessible-megamenu", ":focusable, ." + this.settings.panelClass, $.proxy(_focusOutHandler, this))
                .on("keydown.accessible-megamenu", $.proxy(_keyDownHandler, this))
                .on("mouseover.accessible-megamenu", $.proxy(_mouseOverHandler, this))
                .on("mouseout.accessible-megamenu", $.proxy(_mouseOutHandler, this))
                .on("mousedown.accessible-megamenu", $.proxy(_mouseDownHandler, this))
                .on("click.accessible-megamenu", $.proxy(_clickHandler, this));

            toggleButton.on('click.accessible-megamenu', $.proxy(_clickToggleHandler, this));

            if (isTouch) {
                menu.on("touchmove.accessible-megamenu", $.proxy(_touchmoveHandler, this));
            }

            if ($(document.activeElement).closest(menu).length) {
                $(document.activeElement).trigger("focusin.accessible-megamenu");
            }
        };

        _removeEventHandlers = function () {
            var menu = this.menu,
                toggleButton = this.toggleButton;
            menu.off('.accessible-megamenu');
            if (menu.find('[aria-expanded=true].' + this.settings.panelClass).length) {
                _toggleExpandedEventHandlers.call(this, true);
            }

            toggleButton.off('.accessible-megamenu');
        };

        /* public attributes and methods ------------------------- */
        return {
            constructor: AccessibleMegaMenu,

            /**
             * @lends jQuery.fn.accessibleMegaMenu
             * @desc Initializes an instance of the accessibleMegaMenu plugins
             * @memberof jQuery.fn.accessibleMegaMenu
             * @instance
             */
            init: function () {
                var settings = this.settings,
                    nav = $(this.element),
                    menu = nav.children('ol,ul').first(),
                    topnavitems = menu.children(),
                    toggleButton = nav.children('button').first();
                this.start(settings, nav, menu, topnavitems, toggleButton);
            },

            start: function (settings, nav, menu, topnavitems, toggleButton) {
                var that = this;
                this.settings = settings;
                this.menu = menu;
                this.topnavitems = topnavitems;
                this.toggleButton = toggleButton;

                nav.attr("role", "navigation");
                _addUniqueId.call(that, menu);
                menu.addClass(settings.menuClass);
                menu.addClass(['js', settings.menuClass].join('-'));
                topnavitems.each(function (i, topnavitem) {
                    var topnavitemlink, topnavitempanel;
                    topnavitem = $(topnavitem);
                    topnavitem.addClass(settings.topNavItemClass);
                    topnavitemlink = topnavitem.find(":tabbable:first");
                    topnavitempanel = topnavitem.children(":not(:tabbable):last");
                    _addUniqueId.call(that, topnavitemlink);
                    if (topnavitempanel.length) {
                        _addUniqueId.call(that, topnavitempanel);
                        topnavitemlink.attr({
                            "role": "button",
                            "aria-controls": topnavitempanel.attr("id"),
                            "aria-expanded": false,
                            "tabindex": 0
                        });

                        topnavitempanel.attr({
                                "role": "region",
                                "aria-expanded": false,
                                "aria-hidden": true
                            })
                            .addClass(settings.panelClass)
                            .not("[aria-labelledby]")
                            .attr("aria-labelledby", topnavitemlink.attr("id"));
                    }
                });

                this.panels = menu.find("." + settings.panelClass);

                menu.find("hr").attr("role", "separator");

                toggleButton.addClass(settings.toggleButtonClass);
                toggleButton.attr({
                    'aria-expanded': false,
                    'aria-pressed': false,
                    'aria-controls': menu.attr('id')
                });


                _addEventHandlers.call(this);
            },

            /**
             * @desc Removes maga menu javascript behavior
             * @example $(selector).accessibleMegaMenu("destroy");
             * @return {object}
             * @memberof jQuery.fn.accessibleMegaMenu
             * @instance
             */
            destroy: function () {
                this.menu.removeClass(['js', this.settings.menuClass].join('-'));
                _removeEventHandlers.call(this, true);
            },

            /**
             * @desc Get default values
             * @example $(selector).accessibleMegaMenu("getDefaults");
             * @return {object}
             * @memberof jQuery.fn.accessibleMegaMenu
             * @instance
             */
            getDefaults: function () {
                return this._defaults;
            },

            /**
             * @desc Get any option set to plugin using its name (as string)
             * @example $(selector).accessibleMegaMenu("getOption", some_option);
             * @param {string} opt
             * @return {string}
             * @memberof jQuery.fn.accessibleMegaMenu
             * @instance
             */
            getOption: function (opt) {
                return this.settings[opt];
            },

            /**
             * @desc Get all options
             * @example $(selector).accessibleMegaMenu("getAllOptions");
             * @return {object}
             * @memberof jQuery.fn.accessibleMegaMenu
             * @instance
             */
            getAllOptions: function () {
                return this.settings;
            },

            /**
             * @desc Set option
             * @example $(selector).accessibleMegaMenu("setOption", "option_name",  "option_value",  reinitialize);
             * @param {string} opt - Option name
             * @param {string} val - Option value
             * @param {boolean} [reinitialize] - boolean to re-initialize the menu.
             * @memberof jQuery.fn.accessibleMegaMenu
             * @instance
             */
            setOption: function (opt, value, reinitialize) {
                this.settings[opt] = value;
                if (reinitialize) {
                    this.init();
                }
            }
        };
    }());

    /* lightweight plugin wrapper around the constructor,
       to prevent against multiple instantiations */

    /**
     * @class accessibleMegaMenu
     * @memberOf jQuery.fn
     * @classdesc Implements an accessible mega menu as a jQuery plugin.
     * <p>The mega-menu It is modeled after the mega menu on {@link http://adobe.com|adobe.com} but has been simplified for use by others. A brief description of the interaction design choices can be found in a blog post at {@link http://blogs.adobe.com/accessibility/2013/05/adobe-com.html|Mega menu accessibility on adobe.com}.</p>
     * <h3>Keyboard Accessibility</h3>
     * <p>The accessible mega menu supports keyboard interaction modeled after the behavior described in the {@link http://www.w3.org/TR/wai-aria-practices/#menu|WAI-ARIA Menu or Menu bar (widget) design pattern}, however we also try to respect users' general expectations for the behavior of links in a global navigation. To this end, the accessible mega menu implementation permits tab focus on each of the six top-level menu items. When one of the menu items has focus, pressing the Enter key, Spacebar or Down arrow will open the submenu panel, and pressing the Left or Right arrow key will shift focus to the adjacent menu item. Links within the submenu panels are included in the tab order when the panel is open. They can also be navigated with the arrow keys or by typing the first character in the link name, which speeds up keyboard navigation considerably. Pressing the Escape key closes the submenu and restores focus to the parent menu item.</p>
     * <h3>Screen Reader Accessibility</h3>
     * <p>The accessible mega menu models its use of WAI-ARIA Roles, States, and Properties after those described in the {@link http://www.w3.org/TR/wai-aria-practices/#menu|WAI-ARIA Menu or Menu bar (widget) design pattern} with some notable exceptions, so that it behaves better with screen reader user expectations for global navigation. We don't use <code class="prettyprint prettyprinted" style=""><span class="pln">role</span><span class="pun">=</span><span class="str">"menu"</span></code> for the menu container and <code class="prettyprint prettyprinted" style=""><span class="pln">role</span><span class="pun">=</span><span class="str">"menuitem"</span></code> for each of the links therein, because if we do, assistive technology will no longer interpret the links as links, but instead, as menu items, and the links in our global navigation will no longer show up when a screen reader user executes a shortcut command to bring up a list of links in the page.</p>
     * @example <h4>HTML</h4><hr/>
&lt;nav&gt;
    &lt;ul class=&quot;nav-menu&quot;&gt;
        &lt;li class=&quot;nav-item&quot;&gt;
            &lt;a href=&quot;?movie&quot;&gt;Movies&lt;/a&gt;
            &lt;div class=&quot;sub-nav&quot;&gt;
                &lt;ul class=&quot;sub-nav-group&quot;&gt;
                    &lt;li&gt;&lt;a href=&quot;?movie&amp;genre=0&quot;&gt;Action &amp;amp; Adventure&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&lt;a href=&quot;?movie&amp;genre=2&quot;&gt;Children &amp;amp; Family&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&amp;#8230;&lt;/li&gt;
                &lt;/ul&gt;
                &lt;ul class=&quot;sub-nav-group&quot;&gt;
                    &lt;li&gt;&lt;a href=&quot;?movie&amp;genre=7&quot;&gt;Dramas&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&lt;a href=&quot;?movie&amp;genre=9&quot;&gt;Foreign&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&amp;#8230;&lt;/li&gt;
                &lt;/ul&gt;
                &lt;ul class=&quot;sub-nav-group&quot;&gt;
                    &lt;li&gt;&lt;a href=&quot;?movie&amp;genre=14&quot;&gt;Musicals&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&lt;a href=&quot;?movie&amp;genre=15&quot;&gt;Romance&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&amp;#8230;&lt;/li&gt;
                &lt;/ul&gt;
            &lt;/div&gt;
        &lt;/li&gt;
        &lt;li class=&quot;nav-item&quot;&gt;
            &lt;a href=&quot;?tv&quot;&gt;TV Shows&lt;/a&gt;
            &lt;div class=&quot;sub-nav&quot;&gt;
                &lt;ul class=&quot;sub-nav-group&quot;&gt;
                    &lt;li&gt;&lt;a href=&quot;?tv&amp;genre=20&quot;&gt;Classic TV&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&lt;a href=&quot;?tv&amp;genre=21&quot;&gt;Crime TV&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&amp;#8230;&lt;/li&gt;
                &lt;/ul&gt;
                &lt;ul class=&quot;sub-nav-group&quot;&gt;
                    &lt;li&gt;&lt;a href=&quot;?tv&amp;genre=27&quot;&gt;Reality TV&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&lt;a href=&quot;?tv&amp;genre=30&quot;&gt;TV Action&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&amp;#8230;&lt;/li&gt;
                &lt;/ul&gt;
                &lt;ul class=&quot;sub-nav-group&quot;&gt;
                    &lt;li&gt;&lt;a href=&quot;?tv&amp;genre=33&quot;&gt;TV Dramas&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&lt;a href=&quot;?tv&amp;genre=34&quot;&gt;TV Horror&lt;/a&gt;&lt;/li&gt;
                    &lt;li&gt;&amp;#8230;&lt;/li&gt;
                &lt;/ul&gt;
            &lt;/div&gt;
        &lt;/li&gt;
    &lt;/ul&gt;
&lt;/nav&gt;
     * @example <h4>CSS</h4><hr/>
&#47;* Rudimentary mega menu CSS for demonstration *&#47;

&#47;* mega menu list *&#47;
.nav-menu {
    display: block;
    position: relative;
    list-style: none;
    margin: 0;
    padding: 0;
    z-index: 15;
}

&#47;* a top level navigation item in the mega menu *&#47;
.nav-item {
    list-style: none;
    display: inline-block;
    padding: 0;
    margin: 0;
}

&#47;* first descendant link within a top level navigation item *&#47;
.nav-item &gt; a {
    position: relative;
    display: inline-block;
    padding: 0.5em 1em;
    margin: 0 0 -1px 0;
    border: 1px solid transparent;
}

&#47;* focus/open states of first descendant link within a top level
   navigation item *&#47;
.nav-item &gt; a:focus,
.nav-item &gt; a.open {
    border: 1px solid #dedede;
}

&#47;* open state of first descendant link within a top level
   navigation item *&#47;
.nav-item &gt; a.open {
    background-color: #fff;
    border-bottom: none;
    z-index: 1;
}

&#47;* sub-navigation panel *&#47;
.sub-nav {
    position: absolute;
    display: none;
    top: 2.2em;
    margin-top: -1px;
    padding: 0.5em 1em;
    border: 1px solid #dedede;
    background-color: #fff;
}

&#47;* sub-navigation panel open state *&#47;
.sub-nav.open {
    display: block;
}

&#47;* list of items within sub-navigation panel *&#47;
.sub-nav ul {
    display: inline-block;
    vertical-align: top;
    margin: 0 1em 0 0;
    padding: 0;
}

&#47;* list item within sub-navigation panel *&#47;
.sub-nav li {
    display: block;
    list-style-type: none;
    margin: 0;
    padding: 0;
}
     * @example <h4>JavaScript</h4><hr/>
&lt;!-- include jquery --&gt;
&lt;script src=&quot;http://code.jquery.com/jquery-1.10.1.min.js&quot;&gt;&lt;/script&gt;

&lt;!-- include the jquery-accessibleMegaMenu plugin script --&gt;
&lt;script src=&quot;js/jquery-accessibleMegaMenu.js&quot;&gt;&lt;/script&gt;

&lt;!-- initialize a selector as an accessibleMegaMenu --&gt;
&lt;script&gt;
    $(&quot;nav:first&quot;).accessibleMegaMenu({
        &#47;* prefix for generated unique id attributes, which are required to indicate aria-owns, aria-controls and aria-labelledby *&#47;
        uuidPrefix: &quot;accessible-megamenu&quot;,

        &#47;* css class used to define the megamenu styling *&#47;
        menuClass: &quot;nav-menu&quot;,

        &#47;* css class for a top-level navigation item in the megamenu *&#47;
        topNavItemClass: &quot;nav-item&quot;,

        &#47;* css class for a megamenu panel *&#47;
        panelClass: &quot;sub-nav&quot;,

        &#47;* css class for a group of items within a megamenu panel *&#47;
        panelGroupClass: &quot;sub-nav-group&quot;,

        &#47;* css class for the hover state *&#47;
        hoverClass: &quot;hover&quot;,

        &#47;* css class for the focus state *&#47;
        focusClass: &quot;focus&quot;,

        &#47;* css class for the open state *&#47;
        openClass: &quot;open&quot;
    });
&lt;/script&gt;
     * @param {object} [options] Mega Menu options
     * @param {string} [options.uuidPrefix=accessible-megamenu] - Prefix for generated unique id attributes, which are required to indicate aria-owns, aria-controls and aria-labelledby
     * @param {string} [options.menuClass=accessible-megamenu] - CSS class used to define the megamenu styling
     * @param {string} [options.topNavItemClass=accessible-megamenu-top-nav-item] - CSS class for a top-level navigation item in the megamenu
     * @param {string} [options.panelClass=accessible-megamenu-panel] - CSS class for a megamenu panel
     * @param {string} [options.panelGroupClass=accessible-megamenu-panel-group] - CSS class for a group of items within a megamenu panel
     * @param {string} [options.hoverClass=hover] - CSS class for the hover state
     * @param {string} [options.focusClass=focus] - CSS class for the focus state
     * @param {string} [options.openClass=open] - CSS class for the open state
     * @param {string} [options.openDelay=0] - Open delay when opening menu via mouseover
     * @param {string} [options.closeDelay=250] - Open delay when opening menu via mouseover
     * @param {boolean} [options.openOnMouseover=false] - Should menu open on mouseover
     */
    $.fn[pluginName] = function (options) {
        return this.each(function () {
            var pluginInstance = $.data(this, "plugin_" + pluginName);
            if (!pluginInstance) {
                $.data(this, "plugin_" + pluginName, new $.fn[pluginName].AccessibleMegaMenu(this, options));
            } else if (typeof pluginInstance[options] === 'function') {
                pluginInstance[options].apply(pluginInstance, Array.prototype.slice.call(arguments, 1));
            }
        });
    };

    $.fn[pluginName].AccessibleMegaMenu = AccessibleMegaMenu;

    /* :focusable and :tabbable selectors from
       https://raw.github.com/jquery/jquery-ui/master/ui/jquery.ui.core.js */

    /**
     * @private
     */
    function visible(element) {
        return $.expr.filters.visible(element) && !$(element).parents().addBack().filter(function () {
            return $.css(this, "visibility") === "hidden";
        }).length;
    }

    /**
     * @private
     */
    function focusable(element, isTabIndexNotNaN) {
        var map, mapName, img,
            nodeName = element.nodeName.toLowerCase();
        if ("area" === nodeName) {
            map = element.parentNode;
            mapName = map.name;
            if (!element.href || !mapName || map.nodeName.toLowerCase() !== "map") {
                return false;
            }
            img = $("img[usemap=#" + mapName + "]")[0];
            return !!img && visible(img);
        }
        return (/input|select|textarea|button|object/.test(nodeName) ? !element.disabled :
                "a" === nodeName ?
                element.href || isTabIndexNotNaN :
                isTabIndexNotNaN) &&
            // the element and all of its ancestors must be visible
            visible(element);
    }

    $.extend($.expr[":"], {
        data: $.expr.createPseudo ? $.expr.createPseudo(function (dataName) {
                return function (elem) {
                    return !!$.data(elem, dataName);
                };
            }) : // support: jQuery <1.8
            function (elem, i, match) {
                return !!$.data(elem, match[3]);
            },

        focusable: function (element) {
            return focusable(element, !isNaN($.attr(element, "tabindex")));
        },

        tabbable: function (element) {
            var tabIndex = $.attr(element, "tabindex"),
                isTabIndexNaN = isNaN(tabIndex);
            return (isTabIndexNaN || tabIndex >= 0) && focusable(element, !isTabIndexNaN);
        }
    });
}(jQuery, window, document));
