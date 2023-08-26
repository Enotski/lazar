import CustomStore from "devextreme/data/custom_store";

import { sendRequest } from "../utils/requestUtils";

import moment from "moment";

export const DataGrid = {
    getFilterType: function(filterOperation) {
        var type;
        switch (filterOperation) {
            case "contains":
                {
                    type = 0;
                    break;
                }
            case "notcontains":
                {
                    type = 1;
                    break;
                }
            case "startswith":
                {
                    type = 2;
                    break;
                }
            case "endswith":
                {
                    type = 3;
                    break;
                }
            case "=":
                {
                    type = 4;
                    break;
                }
            case "<>":
                {
                    type = 5;
                    break;
                }
            case "<":
                {
                    type = 6;
                    break;
                }
            case ">":
                {
                    type = 7;
                    break;
                }
            case "<=":
                {
                    type = 8;
                    break;
                }
            case ">=":
                {
                    type = 9;
                    break;
                }
            case "between":
                {
                    type = 10;
                    break;
                }
        }
        return type;
    },
    getFullDateTimeFormat: function() {
        return "dd.MM.yyyy HH:mm:ss";
    },
    getShortDateTimeFormat: function() {
        return "dd.MM.yyyy";
    },
    getCalculateFilterExpression: function(
        filterValue,
        selectedFilterOperation
    ) {
        if (this.dataType === "date" || this.dataType === "datetime") {
            switch (selectedFilterOperation) {
                case "<>":
                    {
                        return [
                            this.dataField,
                            "<>",
                            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
                case "=":
                    {
                        return [
                            this.dataField,
                            "=",
                            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
                case ">":
                    {
                        return [
                            this.dataField,
                            ">",
                            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
                case "<":
                    {
                        return [
                            this.dataField,
                            "<",
                            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
                case ">=":
                    {
                        return [
                            this.dataField,
                            ">=",
                            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
                case "<=":
                    {
                        return [
                            this.dataField,
                            "<=",
                            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
                case "between":
                    {
                        return [
                            this.dataField,
                            "between",
                            moment(filterValue[0]).format("DD.MM.YYYY HH:mm:ss") +
                            ";" +
                            moment(filterValue[1]).format("DD.MM.YYYY HH:mm:ss"),
                        ];
                    }
            }
        } else if (this.dataType === "number") {
            switch (selectedFilterOperation) {
                case "<>":
                    {
                        return [this.dataField, "<>", filterValue];
                    }
                case "=":
                    {
                        return [this.dataField, "=", filterValue];
                    }
                case ">":
                    {
                        return [this.dataField, ">", filterValue];
                    }
                case "<":
                    {
                        return [this.dataField, "<", filterValue];
                    }
                case ">=":
                    {
                        return [this.dataField, ">=", filterValue];
                    }
                case "<=":
                    {
                        return [this.dataField, "<=", filterValue];
                    }
                case "between":
                    {
                        return [
                            this.dataField,
                            "between",
                            filterValue[0] + ";" + filterValue[1],
                        ];
                    }
            }
        }
        return this.defaultCalculateFilterExpression.apply(this, arguments);
    },
    getColumnLookup: function(url, type, params) {
        return {
            dataSource: new CustomStore({
                load: async function() {
                    type = type !== null ? type : 'POST';
                    return await sendRequest(url, type, params)
                        .then(async function(data) {
                            let options = [];
                            data.forEach(function(item) {
                                options.push(item.Text);
                            });
                            return options;
                        })
                        .catch(() => {
                            throw new Error("Data Loading Error");
                        });
                },
            }),
        };
    },
};