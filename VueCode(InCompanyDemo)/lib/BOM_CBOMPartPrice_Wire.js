new Vue({
    el: '#vueapp',
    data: function () {
        return {
            initComplete: false,
            formData: {
                EBP_PartRow: $("#txtID").val(),
                EBP_Year: '',
                EBP_Quarter: '',
                EBP_WireFactory: '',
                EBP_AreaType: '',
                PriceModel: {},
                DefaultRate: {},
                Equipments: [],
                DeletedEquipmentRows: []
            }
        };
    },
    mounted: function () {
        this.getData()
    },
    watch: {
        //formData: {
        //    handler(curVal, oldVal) {
        //        console.log('watch', curVal)
        //        this.cal();
        //    },
        //    //deep: true
        //}
    },
    filter: {
        numberFormat: function (value, format) {
            return numeral(value).format(format);
        }
    },
    methods: {
        getData: function () {
            var me = this
            $.ajax({
                type: "post",
                url: 'Ajax/BOM_CBOMPartPrice.aspx?method=getpartprice',
                data: {
                    PartRow: me.formData.EBP_PartRow
                },
                success: function (e) {
                    me.formData.EBP_Year = e.Content.EBP_Year;
                    me.formData.EBP_Quarter = e.Content.EBP_Quarter;
                    me.formData.EBP_WireFactory = e.Content.EBP_WireFactory;
                    me.formData.EBP_AreaType = e.Content.EBP_AreaType;
                    me.formData.PriceModel = e.Content.PriceModel;
                    me.formData.DefaultRate = e.Content.DefaultRate;
                    me.formData.Equipments = e.Content.Equipments;
                },
                error: function (e) {
                    //layer.closeAll();
                    $.messager.alert("BOMone! System", "加载失败，请与管理员联系！", "error");
                }
            })
        },
        cancel: function() {
            window.parent.closeDialog(false);
        },
        //保存
        save: function() {
            var me = this
            $.ajax({
                type: "post",
                url: 'Ajax/BOM_CBOMPartPrice.aspx?method=save',
                data: {
                    partRow: me.formData.EBP_PartRow,
                    json: JSON.stringify(me.formData)
                },
                success: function (e) {
                    window.parent.setPartModelPrice(me.formData.PriceModel.EBP_PartPrice);
                    window.parent.closeDialog(true);
                },
                error: function (e) {
                    //layer.closeAll();
                    $.messager.alert("BOMone! System", "保存失败，请与管理员联系！", "error");
                }
            })
        },
        //添加设备
        addEquipment: function () {
            this.formData.Equipments.push({
                EBP_Equipment_Row: 0,
                EBP_CostRate: 0,
                EBP_RateType: '',
                EBP_Qty: undefined,
                EBP_Adjust: undefined,
                EBP_Hours: 0,
                EBP_MadeAmount: 0,
                EBP_MadeRate: this.formData.DefaultRate.EBP_MadeRate
            });
        },
        //删除设备
        removeEquipment: function (index) {
            var id = this.formData.Equipments[index].EBP_Row;
            if (id > 0) {
                this.formData.DeletedEquipmentRows.push(id);
            }
            this.formData.Equipments.splice(index, 1);
        },
        onYearChanged: function () {
            this.getMaterialData();
            this.formData.Equipments.forEach(item => {
                this.getEquipmentData(item);
            });
        },
        onQuarterChanged: function () {
            this.getMaterialData();
        },
        onWireFactoryChanged: function () {
            this.getMaterialData();
        },
        //获取原材料价格
        getMaterialData: function () {
            var me = this
            if (!me.formData.PriceModel.EBP_Material_Row
                || !me.formData.EBP_Year
                || !me.formData.EBP_Quarter
                || !me.formData.EBP_WireFactory) {
                return;
            }

            $.ajax({
                type: "post",
                url: 'Ajax/BOM_CBOMPartPrice.aspx?method=getmaterialdata',
                data: {
                    ml_row: me.formData.PriceModel.EBP_Material_Row,
                    year: me.formData.EBP_Year,
                    quarter: me.formData.EBP_Quarter,
                    factory: me.formData.EBP_WireFactory
                },
                success: function (e) {
                    me.formData.PriceModel.EBP_MaterialPrice = e.Content.Price;
                    me.cal();
                },
                error: function (e) {
                    //layer.closeAll();
                    $.messager.alert("BOMone! System", "保存失败，请与管理员联系！", "error");
                }
            })
        },
        //获取设备信息
        getEquipmentData: function (equipment) {
            var me = this
            if (!equipment.EBP_Equipment_Row
                || !me.formData.EBP_Year
                || !me.formData.EBP_AreaType) {
                return;
            }

            $.ajax({
                type: "post",
                url: 'Ajax/BOM_CBOMPartPrice.aspx?method=getequipmentdata',
                data: {
                    eq_row: equipment.EBP_Equipment_Row,
                    year: me.formData.EBP_Year,
                    areatype: me.formData.EBP_AreaType
                },
                success: function (e) {
                    equipment.EBP_RateType = e.Content.RateType;
                    equipment.EBP_CostRate = e.Content.CostRate;
                    me.calEquipment(equipment);
                },
                error: function (e) {
                    //layer.closeAll();
                    $.messager.alert("BOMone! System", "保存失败，请与管理员联系！", "error");
                }
            })
        },
        //计算设备制费
        calEquipment: function (equipment) {
            if (isNaN(parseFloat(equipment.EBP_Qty))
                || isNaN(parseFloat(equipment.EBP_CostRate))) {
                return;
            }

            if (equipment.EBP_RateType == 'time') {
                equipment.EBP_Hours = equipment.EBP_CostRate * equipment.EBP_Qty;
                equipment.EBP_MadeAmount = numeral(equipment.EBP_Hours / 3600 / equipment.EBP_MadeRate).format('0.000');
            } else {
                equipment.EBP_MadeAmount = numeral(equipment.EBP_CostRate * equipment.EBP_Qty).format('0.000');
                equipment.EBP_Hours = "";
            }
            this.cal();
        },
        //价格计算
        cal: function () {
            var me = this
            var model = this.formData.PriceModel;

            var madeamount = 0;

            this.formData.Equipments.forEach(item => {
                var madeamt = parseFloat(item.EBP_MadeAmount);
                if (!isNaN(madeamt)) {
                    madeamount += madeamt;
                }
            })

            model.EBP_MadeAmount = numeral(madeamount).format('0.000');

            model.EBP_MaterialAmount = numeral(model.EBP_MaterialPrice * model.EBP_PartMeasure * model.EBP_MaterialRate).format('0.000');
            //model.EBP_MadeAmount = numeral(model.EBP_TotalHours / 3600 / model.EBP_MadeRate).format('0.000');
            model.EBP_ScrapAmount = numeral((parseFloat(model.EBP_MaterialAmount) + parseFloat(model.EBP_MadeAmount)) * model.EBP_ScrapRate).format('0.000');
            model.EBP_PackageAmount = numeral((parseFloat(model.EBP_MaterialAmount) + parseFloat(model.EBP_MadeAmount)) * model.EBP_PackageRate).format('0.000');
            model.EBP_ManageAmount = numeral((parseFloat(model.EBP_MaterialAmount) + parseFloat(model.EBP_MadeAmount)) * model.EBP_ManageRate).format('0.000');
            model.EBP_ProfitAmount = numeral((parseFloat(model.EBP_MaterialAmount) + parseFloat(model.EBP_MadeAmount)) * model.EBP_ProfitRate).format('0.000');
            model.EBP_PartPrice = numeral(parseFloat(model.EBP_MaterialAmount)
                + parseFloat(model.EBP_MadeAmount)
                + parseFloat(model.EBP_ScrapAmount)
                + parseFloat(model.EBP_PackageAmount)
                + parseFloat(model.EBP_ManageAmount)
                + parseFloat(model.EBP_ProfitAmount)).format('0.000');
        },
        //重置费率
        resetRate: function () {
            var me = this;
            $.messager.confirm("确认", "确认要重置所有费率吗？", function (r) {
                me.formData.PriceModel.EBP_MaterialRate = me.formData.DefaultRate.EBP_MaterialRate;
                me.formData.PriceModel.EBP_MadeRate = me.formData.DefaultRate.EBP_MadeRate;
                me.formData.PriceModel.EBP_ScrapRate = me.formData.DefaultRate.EBP_ScrapRate;
                me.formData.PriceModel.EBP_PackageRate = me.formData.DefaultRate.EBP_PackageRate;
                me.formData.PriceModel.EBP_ManageRate = me.formData.DefaultRate.EBP_ManageRate;
                me.formData.PriceModel.EBP_ProfitRate = me.formData.DefaultRate.EBP_ProfitRate;
                me.cal();
            });
        },
        //重置制费费率
        resetMadeRate: function() {
            var me = this;
            $.messager.confirm("确认", "确认要重置所有设备制费费率吗？", function (r) {
                me.formData.Equipments.forEach(item => {
                    item.EBP_MadeRate = me.formData.DefaultRate.EBP_MadeRate;
                    me.calEquipment(item);
                })
                me.cal();
            });
        },
        //材料费费率
        editMaterialRate() {
            var me = this;
            layer.prompt({ title: '材料报废率', formType: 0, value: this.formData.PriceModel.EBP_MaterialRate }, function (value, index) {
                layer.close(index);
                if (value <= 0) {
                    $.messager.alert("消息", "材料报废率必须大于0！");
                } else {
                    me.formData.PriceModel.EBP_MaterialRate = value;
                    me.cal();
                }
            });
        },
        //制费费率
        editMadeRate() {
            var me = this;
            layer.prompt({ title: '制费费率', formType: 0, value: this.formData.PriceModel.EBP_MadeRate }, function (value, index) {
                layer.close(index);
                if (value <= 0) {
                    $.messager.alert("消息", "制费费率必须大于0！");
                } else {
                    me.formData.PriceModel.EBP_MadeRate = value;
                    me.cal();
                }
            });
        },
        //报废费率
        editScrapRate() {
            var me = this;
            layer.prompt({ title: '报废费率', formType: 0, value: this.formData.PriceModel.EBP_ScrapRate }, function (value, index) {
                layer.close(index);
                if (value <= 0) {
                    $.messager.alert("消息", "报废费率必须大于0！");
                } else {
                    me.formData.PriceModel.EBP_ScrapRate = value;
                    me.cal();
                }
            });
        },
        //包装运输费率
        editPackageRate() {
            var me = this;
            layer.prompt({ title: '包装运输费率', formType: 0, value: this.formData.PriceModel.EBP_PackageRate }, function (value, index) {
                layer.close(index);
                if (value <= 0) {
                    $.messager.alert("消息", "包装运输费率必须大于0！");
                } else {
                    me.formData.PriceModel.EBP_PackageRate = value;
                    me.cal();
                }
            });
        },
        //管理费率
        editManageRate() {
            var me = this;
            layer.prompt({ title: '管理费率', formType: 0, value: this.formData.PriceModel.EBP_ManageRate }, function (value, index) {
                layer.close(index);
                if (value <= 0) {
                    $.messager.alert("消息", "管理费率必须大于0！");
                } else {
                    me.formData.PriceModel.EBP_ManageRate = value;
                    me.cal();
                }
            });
        },
        //利润率
        editProfitRate() {
            var me = this;
            layer.prompt({ title: '利润率', formType: 0, value: this.formData.PriceModel.EBP_ProfitRate }, function (value, index) {
                layer.close(index);
                if (value <= 0) {
                    $.messager.alert("消息", "利润率必须大于0！");
                } else {
                    me.formData.PriceModel.EBP_ProfitRate = value;
                    me.cal();
                }
            });
        }
    }
})