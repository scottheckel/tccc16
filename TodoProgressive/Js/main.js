(function () {
    var Todo = Backbone.Model.extend({
        idAttribute: 'TodoId'
    });
    var TodoCollection = Backbone.Collection.extend({
        model: Todo,
        url: '/api/todo',
        defaults: {
            TodoId: null,
            Text: '',
            IsComplete: false,
            Location: ''
        }
    });

    /* INSERT SCRIPTS */
    var Todos = new TodoCollection();
    var TodosListView = Backbone.View.extend({
        el: '#app',
        template: _.template($('#todoTemplate').html()),
        events: {
            'click .markComplete': 'markCompleteClick',
            'click .createNew': 'createNewClick'
        },
        createNewClick: function (event) {
            Todos.create({
                'Text': $('#text').val()
            });
            event.preventDefault();
        },
        markCompleteClick: function (event) {
            var $targetTodo = $(event.currentTarget).closest('li');
            var todo = Todos.get($targetTodo.data('todoid'));
            todo.set('IsComplete', true);
            todo.save();
            this.render();

            // Placing on the bottom for JS errors
            event.preventDefault();
        },
        initialize: function (args) {
            $('#todosList li').each(function () {
                var $todoItem = $(this);
                Todos.add({
                    'TodoId': $todoItem.data('todoid'),
                    'IsComplete': $todoItem.data('iscomplete'),
                    'Location': $todoItem.data('location'),
                    'Text': $todoItem.find('.todo').html()
                });
            });
            Todos.on('add', this.renderOne, this);
            Todos.on('reset', this.render, this);
        },
        render: function () {
            this.$el.find('#todosList').html("");
            Todos.each(this.renderOne, this);
        },
        renderOne: function (todo) {
            this.$el.find('#todosList').append(this.template(todo.toJSON()));
        }
    });
    var todosView = new TodosListView();
})();