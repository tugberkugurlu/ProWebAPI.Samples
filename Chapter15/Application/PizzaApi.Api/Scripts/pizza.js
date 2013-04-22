(function(window, ko, _, $) {

    if (!ko) {
        throw "Knockoutjs library was not found.";
    }

    if (!_) {
        throw "underscorejs library was not found.";
    }

    if (!$) {
        throw "jquery library was not found.";
    }


    var     NEW_ORDER = "New order",
           EDIT_ORDER = "Editing #",
           PLACE_ORDER = "Place order",
           UPDATE_ORDER = "Update order",
           rootUrl = window.location.href;

    if (rootUrl.indexOf("/#"))
        rootUrl = rootUrl.replace("/#", "");
    rootUrl += "/api/Order";

    function namespace(namespaceString) {
        var parts = namespaceString.split('.'),
            parent = window,
            currentPart = '';

        for (var i = 0, length = parts.length; i < length; i++) {
            currentPart = parts[i];
            parent[currentPart] = parent[currentPart] || {};
            parent = parent[currentPart];
        }

        return parent;
    }

    namespace("ProAspNetWebApi.Samples.UnitTesting.Pizza");

    ProAspNetWebApi.Samples.UnitTesting.Pizza.MainViewModel = function MainViewModel() {

        var that = this;
        this.rootUrl = rootUrl;
        this.orders = ko.observableArray();
        this.customerName = ko.observable();
        this.orderTitle = ko.observable(NEW_ORDER);
        this.buttonText = ko.observable(PLACE_ORDER);

        this.PizzaMenu = [
            "Hawaiian",
            "Meat Feast",
            "Spicy Bacon",
            "Inferno",
            "Vegetarian",
            "Four Season",
            "Ham and Mushroom",
            "Pepperoni"
        ];
        this.currentOrderItems = ko.observableArray(_.map(this.PizzaMenu, function(x) {
            return new ProAspNetWebApi.Samples.UnitTesting.Pizza.OrderItem(x);
        }));
        
        this.canAdd = ko.computed(function () {
            var anyPizzaChosen = _.reduce(that.currentOrderItems(), function(memo, x) { return memo + x.quantity(); }, 0) > 0,
                customerHasName = that.customerName() != undefined && that.customerName()!="";
            return anyPizzaChosen && customerHasName;
             
        });

        this.deleteOrder = function() {
            that.orders.remove(this);
        };

    };

    ProAspNetWebApi.Samples.UnitTesting.Pizza.MainViewModel.prototype.up = function() {
        this.quantity(this.quantity()+1);
    };

    ProAspNetWebApi.Samples.UnitTesting.Pizza.MainViewModel.prototype.down = function () {
        this.quantity(Math.max(this.quantity()-1,0));
    };

    ProAspNetWebApi.Samples.UnitTesting.Pizza.MainViewModel.prototype.placeOrder = function () {
        var that = this;
        if (this.buttonText() == PLACE_ORDER) {
            // New order
            $.ajax({
                type: "POST",
                data: this._buildOrders(),
                url: this.rootUrl
                })
                .success(function(data, text, xhr) {
                    that.addOrder(xhr.getResponseHeader("Location"));
                });
                ;
        } else {
            
        }
            
    };
    
   

    ProAspNetWebApi.Samples.UnitTesting.Pizza.MainViewModel.prototype.addOrder = function addOrder(location) {
        var that = this;
        $.ajax({ url: location })
            .success(function (data, text, xhr) {
                that.orders.push(new ProAspNetWebApi.Samples.UnitTesting.Pizza.Order(data));
            });
    };

    ProAspNetWebApi.Samples.UnitTesting.Pizza.MainViewModel.prototype._buildOrders = function () {
        return new ProAspNetWebApi.Samples.UnitTesting.Pizza.OrderDto(this.customerName,
            _.reduce(this.currentOrderItems(), 
                function(memo, x) {
                    if (x.quantity())
                        memo.push(new ProAspNetWebApi.Samples.UnitTesting.Pizza.OrderItemDto(x.name(), x.quantity()));
                    return memo;
                }, [] )
            );
    };
    
    ProAspNetWebApi.Samples.UnitTesting.Pizza.Order = function Order(data) {
        var that = this;
        this.items =ko.observableArray(data.items);
        this.id = ko.observable(data.id);
        this.customerName = ko.observable(data.customerName);
        this.totalPrice = ko.observable(data.totalPrice);
        this.totalPriceFormatted = ko.computed(function() {
            return that.totalPrice().toFixed(2) + " $";
        });
    };
    
    ProAspNetWebApi.Samples.UnitTesting.Pizza.OrderDto = function OrderDto(customerName, items) {
        this.items = items;
        this.id = 0;
        this.customerName = customerName;
        this.totalPrice = 0;
    };

    ProAspNetWebApi.Samples.UnitTesting.Pizza.OrderItem = function OrderItem(name) {
        this.name = ko.observable(name);
        this.quantity = ko.observable(0);
    };
    
    ProAspNetWebApi.Samples.UnitTesting.Pizza.OrderItemDto = function OrderItemDto(name, quantity) {
        this.name = name;
        this.quantity = quantity;
    };

})(this, ko, _, $);