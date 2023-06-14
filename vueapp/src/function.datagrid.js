window.ResizeFunctions = [];
ResizeAllDataGrids = function () {
    window.ResizeFunctions.forEach(function (func) {
        func();
    });
};
window.onresize = ResizeAllDataGrids;
// Сохранение настроек таблиц 
var setStorageData = function (tableId, tableGuid) {
    // Устанавливается таймаут так, как запись изменений в localstorage происходит не сразу
    Vue.$snotify.async('Сохранение настроек', 'Сохранение...', () => new Promise((resolve, reject) => {
        setTimeout(function () {
            var tableStoredData = localStorage.getItem(tableId);
            $.ajax({
                url: "/Base/SetUserStoredData",
                method: "POST",
                data: { tableId: tableGuid, tableData: tableStoredData },
                cache: false,
            }).done(function (response) {
                if (response && response.State === 0) {
                    resolve({
                        title: 'Готово',
                        body: 'Настройки сохранены!',
                        config: {
                            timeout: 2000,
                            closeOnClick: true
                        },
                        position: "rightBottom",
                    });
                } else {
                    var id = Vue.$generateRandonIntId();
                    var errorHtmlBody = '<div class="snotifyToast__title">Ошибка</div><div id="' + id +
                        '" class="snotifyToast__body">' + response.ErrorMsg +
                        '</div><div class="snotify-icon snotify-icon--error"></div>';
                    reject({
                        html: errorHtmlBody,
                        config: {
                            timeout: 2000,
                            closeOnClick: true
                        },
                        position: "rightBottom",
                        buttons: [{
                            text: "СКОПИРОВАТЬ В БУФЕР", className: "mdi mdi-content-copy", action: function () {
                                Vue.$copyToBuffer(id);
                            }, bold: true
                        }]
                    })
                }
            });
        }, 3000);
    }));
    
};

var DataGrid = {
    detectIE: function () {
        var ua = window.navigator.userAgent;

        var msie = ua.indexOf('MSIE ');
        if (msie > 0) {
            return true;
        }
        var trident = ua.indexOf('Trident/');
        if (trident > 0) {
            return true;
        }
        return false;
    },
    ie_customCellTemplate: null,
    ie_cellTitle: function (element, info) {
        if (DataGrid.detectIE()) {
            element[0].selector = true;
            element[0].dataset.originalTitle = info.text;
            element[0].textContent = info.text;

            element.tooltip();
            if (!!DataGrid.ie_customCellTemplate)
                DataGrid.ie_customCellTemplate(element, info);
        }
    },
    getFilterType: function (filterOperation) {
        var type;
        switch (filterOperation) {
            case "contains": {
                type = 0;
                break;
            }
            case "notcontains": {
                type = 1;
                break;
            }
            case "startswith": {
                type = 2;
                break;
            }
            case "endswith": {
                type = 3;
                break;
            }
            case "=": {
                type = 4;
                break;
            }
            case "<>": {
                type = 5;
                break;
            }
            case "<": {
                type = 6;
                break;
            }
            case ">": {
                type = 7;
                break;
            }
            case "<=": {
                type = 8;
                break;
            }
            case ">=": {
                type = 9;
                break;
            }
            case "between": {
                type = 10;
                break;
            }
        }
        return type;
    },
    groupCellTemplateRender: function (element) {
        return element.value ? "Да" : "Нет";
    },
    getFullDateTimeFormat: function () {
        return "dd.MM.yyyy HH:mm:ss";
    },
    getFullDateTimeFormatWithMilliseconds: function () {
        return "dd.MM.yyyy HH:mm:ss.sssssss";
    },
    getShortDateTimeFormat: function () {
        return "dd.MM.yyyy";
    },
    getCalculateCustomSummary: function (options) {
        if (options.name === "text") {
            if (options.summaryProcess === "start") {
                options.totalValue = "";
            }
            if (options.summaryProcess === "calculate") {
                options.totalValue = options.value;
            }
        }
        if (options.name === "counter") {
            if (options.summaryProcess === "start") {
                options.totalValue = 0;
            }
            if (options.summaryProcess === "calculate") {
                options.totalValue += 1;
            }
        }
    },
    getCalculateFilterExpression: function (filterValue, selectedFilterOperation) {
        if (this.dataType === "date" || this.dataType === "datetime") {
            switch (selectedFilterOperation) {
                case "<>": {
                    return [this.dataField, "<>", moment(filterValue).format("DD.MM.YYYY HH:mm:ss")];
                }
                case "=": {
                    return [this.dataField, "=", moment(filterValue).format("DD.MM.YYYY HH:mm:ss")];
                }
                case ">": {
                    return [this.dataField, ">", moment(filterValue).format("DD.MM.YYYY HH:mm:ss")];
                }
                case "<": {
                    return [this.dataField, "<", moment(filterValue).format("DD.MM.YYYY HH:mm:ss")];
                }
                case ">=": {
                    return [this.dataField, ">=", moment(filterValue).format("DD.MM.YYYY HH:mm:ss")];
                }
                case "<=": {
                    return [this.dataField, "<=", moment(filterValue).format("DD.MM.YYYY HH:mm:ss")];
                }
                case "between": {
                    return [this.dataField, "between", moment(filterValue[0]).format("DD.MM.YYYY HH:mm:ss") + ';' + moment(filterValue[1]).format("DD.MM.YYYY HH:mm:ss")];
                }
            }
        } else if (this.dataType === "number") {
            switch (selectedFilterOperation) {
                case "<>": {
                    return [this.dataField, "<>", filterValue];
                }
                case "=": {
                    return [this.dataField, "=", filterValue];
                }
                case ">": {
                    return [this.dataField, ">", filterValue];
                }
                case "<": {
                    return [this.dataField, "<", filterValue];
                }
                case ">=": {
                    return [this.dataField, ">=", filterValue];
                }
                case "<=": {
                    return [this.dataField, "<=", filterValue];
                }
                case "between": {
                    return [this.dataField, "between", filterValue[0] + ';' + filterValue[1]];
                }
            }
        }
        return this.defaultCalculateFilterExpression.apply(this, arguments);
    },
    getColumnLookup: function (url, data) {
        return {
            dataSource: new DevExpress.data.CustomStore({
                loadMode: "raw",
                load: function () {
                    var d = $.Deferred();
                    $.ajax({
                        type: 'POST',
                        url: url,
                        data: data,
                        success: function (response) {
                            var options = [];
                            response.Result.forEach(function (item) {
                                options.push(item.Text);
                            });
                            d.resolve(options);
                        },
                        error: function () {
                            d.reject();
                        }
                    });
                    return d.promise();
                }.bind(this)
            })
        }
    },
    createTable: function (selector, columns, url, data, events, options, locateInModal, keyExpr, groupSummary) {
        if (!events) {
            events = {};
        }
        if (!options) {
            options = {};
        }
        if (options.noDataMess === undefined || options.noDataMess === null) {
            options.noDataMess = "Нет данных";
        }
        if (!locateInModal) {
            locateInModal = false;
        }
        if (DataGrid.detectIE()) {
            columns.forEach(function (col) {
                if (col.dataField !== "Files" && col.dataField !== "Conversation")
                    col.cellTemplate = DataGrid.ie_cellTitle;
            });
        }
        DevExpress.localization.locale(navigator.language);
        var gridDataSource = new DevExpress.data.DataSource({
            key: keyExpr != null ? keyExpr : "Id",
            load: function (loadOptions) {
                var d = $.Deferred(),
                    sorts = [],
                    filters = [];
                if (!!loadOptions['sort']) {
                    loadOptions['sort'].forEach(function (item) {
                        sorts.push({ ColumnName: item.selector, Type: item.desc ? 1 : 0 });
                    });
                }
                if (!!loadOptions['filter']) {
                    if (loadOptions['filter'].hasOwnProperty("filterValue")) {
                        filters.push({ ColumnName: loadOptions['filter'][0], Type: DataGrid.getFilterType(loadOptions['filter'][1]), Value: loadOptions['filter'][2] });
                    } else {
                        loadOptions['filter'].filter(function (item) { return item !== "and" && item !== "or" }).forEach(function (item) {
                            filters.push({ ColumnName: item[0], Type: DataGrid.getFilterType(item[1]), value: item[2] });
                        });
                    }
                }
                var params = {
                    skip: loadOptions['skip'] || 0,
                    take: loadOptions['take'] || 1000,
                    sorts: sorts,
                    filters: filters
                };
                if (!!data) {
                    switch (typeof data) {
                        case "object": {
                            params = Object.assign({}, params, data);
                            break;
                        }
                        case "function": {
                            params = Object.assign({}, params, data());
                            break;
                        }
                    }

                }
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: params,
                    success: function (response) {
                        d.resolve(response.data, {
                            totalCount: response.totalCount,
                            summary: response.summary,
                            groupCount: response.groupCount
                        });
                    },
                    error: function () {
                        d.reject("Ошибка загрузки данных");
                    }
                });
                return d.promise();
            }
        });

        var getHeight = function (selector) {
            return !!options.height ? options.height : (locateInModal ? 500 : window.innerHeight - $(selector).offset().top - 20);
        };
        var getWidth = function (selector) {
            return !!options.width ? options.width : $(selector).parent().width();
        };

        var table = $(selector).dxDataGrid({
            dataSource: gridDataSource,
            showColumnLines: !!options.showColumnLines ? options.showColumnLines : true,
            showRowLines: !!options.showRowLines ? options.showRowLines : true,
            rowAlternationEnabled: !!options.rowAlternationEnabled ? options.rowAlternationEnabled : true,
            showBorders: !!options.showBorders ? options.showBorders : true,
            paging: !!options.paging ? options.paging : {
                pageSize: 1000,
                pageIndex: 0,    // Shows the second page
                enabled: true
            },
            scrolling: {
                //useNative: true
            },
            noDataText: options.noDataMess,
            filterRow: !!options.filterRow ? options.filterRow : {
                visible: true,
                applyFilter: "auto" // or "onClick"
            },
            headerFilter: !!options.headerFilter ? options.headerFilter : {
                visible: false,
            },
            pager: !!options.pager ? options.pager : {
                showPageSizeSelector: true,
                allowedPageSizes: [50, 100, 200, 1000],
                showNavigationButtons: true,
                visible: true
            },
            selection: !!options.selection ? options.selection : {
                mode: "multiple",
                showCheckBoxesMode: "none"
                //selectAllMode: "page" // or "allPages"
            },
            columnChooser: !!options.columnChooser ? options.columnChooser : {
                enabled: false,
                mode: "select"
            },
            onContextMenuPreparing: function (e) {
                if (e.target === "header") {
                    if (e.row.rowType === "filter") {
                        if (!e.items) e.items = [];
                        e.items.push({
                            text: "Сбросить фильтры",
                            onItemClick: function () {
                                table.clearFilter();
                            }
                        });
                    }
                    if (e.row.rowType === "header") {
                        if (!e.items) e.items = [];
                        e.items.push({
                            text: "Выбор столбцов",
                            onItemClick: function () {
                                table.showColumnChooser();
                            }
                        });
                    }
                    if (options.tableGuid !== null && options.tableGuid !== undefined && e.row.rowType === "header") {
                        if (!e.items) e.items = [];
                        e.items.push({
                            text: "Сохранить настройки таблицы",
                            onItemClick: function () {
                                setStorageData(selector, options.tableGuid);
                            }
                        });
                    }
                }
            },
            onCellClick: function (e) {
                if (!!events.onCellClick) {
                    events.onCellClick(e);
                }
            },
            onCellDblClick: function (e) {
                Vue.$copyTextContent(e.cellElement[0]);
                if (!!events.onCellDblClick) {
                    events.onCellDblClick(e);
                }
            },
            onContentReady: function (e) {
                if (e.component.totalCount() == 0) {
                    var filters = e.component.getCombinedFilter();
                    if (filters) {
                        e.component.option("noDataText", "Записей, удовлетворяющих условиям поиска, не найдено. Для отображения всех записей необходимо убрать фильтр");
                    } else {
                        e.component.option("noDataText", options.noDataMess);
                    }
                }

                // стилизация текста
                mixinBaseInitVue.methods.updateTableAppearanceStyle();

                var columnChooserView = e.component.getView("columnChooserView");
                if (!columnChooserView._popupContainer) {
                    columnChooserView._initializePopupContainer();
                    columnChooserView.render();
                    columnChooserView._popupContainer.option("position", { of: e.element, my: "center", at: "center" });
                }
                if (!!events.onContentReady) {
                    events.onContentReady(e);
                }
            },
            stateStoring: {
                enabled: true,
                type: "custom",
                customLoad: function () {
                    var state = JSON.parse(localStorage.getItem(selector));
                    return state;
                },
                customSave: function (state) {
                    if (state.hasOwnProperty("selectedRowKeys")) {
                        delete state.selectedRowKeys;
                    }
                    if (state.hasOwnProperty("allowedPageSizes")) {
                        delete state.allowedPageSizes;
                    }
                    localStorage.setItem(selector, JSON.stringify(state));
                }
            },
            sorting: !!options.sorting ? options.sorting : {
                mode: "multiple"
            },
            width: getWidth(selector),
            columnWidth: !!options.columnWidth ? options.columnWidth : 200,
            columnResizingMode: !!options.columnResizingMode ? options.columnResizingMode : "widget",
            remoteOperations: !!options.remoteOperations ? options.remoteOperations : {
                paging: true,
                filtering: true,
                sorting: true,
                grouping: false,
                summary: true
            },
            allowColumnReordering: (options.allowColumnReordering != null && options.allowColumnReordering != undefined) ? options.allowColumnReordering : true,
            allowColumnResizing: (options.allowColumnResizing != null && options.allowColumnResizing != undefined) ? options.allowColumnResizing : true,
            columnAutoWidth: (options.columnAutoWidth != null && options.columnAutoWidth != undefined) ? options.columnAutoWidth : true,
            height: getHeight(selector),
            columns: columns,
            summary: Object.assign({}, groupSummary, { calculateCustomSummary: DataGrid.getCalculateCustomSummary }),
            grouping: {
                contextMenuEnabled: false,
                autoExpandAll: true,
                texts: {
                    groupContinuesMessage: '',
                    groupContinuedMessage: ''
                }
            },
            columnFixing: {
                enabled: !DataGrid.detectIE()
            },
            onInitialized: function (e) {
                if (!!events.onInitialized) {
                    events.onInitialized(e);
                }
            },
            onRowClick: function (e) {
                if (!!events.onRowClick) {
                    events.onRowClick(e);
                }
            },
            onRowDblClick: function (e) {
                if (!!events.onRowDblClick) {
                    events.onRowDblClick(e);
                }
            },
            onSelectionChanged: function (e) {
                if (!!events.onSelectionChanged) {
                    events.onSelectionChanged(e);
                }
            },
            onInitNewRow: function (e) {
                if (!!events.onInitNewRow) {
                    events.onInitNewRow(e);
                }
            },
            onRowPrepared: function (e) {
                if (!!events.onRowPrepared) {
                    events.onRowPrepared(e);
                }
            },
            onRowUpdated: function (e) {
                if (!!events.onRowUpdated) {
                    events.onRowUpdated(e);
                }
            },
            onOptionChanged: function (e) {
                if (!!events.onOptionChanged) {
                    events.onOptionChanged(e);
                }
            },
        }).dxDataGrid("instance");

        var resizeTable = function () {
            if (table.element().is(':visible')) {
                table.option("height", getHeight(selector));
                table.option("width", getWidth(selector));
            }
        };

        table.resizeTable = resizeTable;
        window.ResizeFunctions.push(table.resizeTable);
        return table;
    }
};