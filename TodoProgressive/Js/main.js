(function () {
    // A Todo Model
    var Todo = Backbone.Model.extend({
        idAttribute: 'TodoId',
        defaults: {
            TodoId: null, // unique identifier
            Text: '', // todo text
            IsComplete: false, // is it complete?
            Location: '' // geo location (not used... remnant of an older, longer demo)
        }
    });

    // A collection of Todos
    var TodoCollection = Backbone.Collection.extend({
        model: Todo,
        url: '/api/todo'
    });

    /* INSERT SCRIPTS */
    var Todos = new TodoCollection();
    var TodosListView = Backbone.View.extend({
        el: '#app',
        template: _.template($('#todoTemplate').html()), // Save our individual Todo item template
        events: { 
            'click .markComplete': 'markCompleteClick', // subscribe to the "mark complete" link click
            'click .createNew': 'createNewClick' // subscribe to the new todo form submit click (note: this probably should actually look for the submit not the input click)
        },
        createNewClick: function (event) {
            // Create (and save) our new Todo item
            Todos.create({
                'Text': $('#text').val()
            });

            // Placing on the bottom in case of JS errors, prevent the default link click
            event.preventDefault();
        },
        markCompleteClick: function (event) {
            // Find the todo we clicked on to mark complete
            var $targetTodo = $(event.currentTarget).closest('li');
            var todo = Todos.get($targetTodo.data('todoid'));

            // Make changes and save to the server
            todo.set('IsComplete', true);
            todo.save();

            // Rerender everything (yes we really should just re-render one but this is an easier demo)
            this.render();

            // Placing on the bottom in case of JS errors, prevent the default link click
            event.preventDefault();
        },
        initialize: function (args) {
            // Hydrate our Todo collection with the content on our page
            $('#todosList li').each(function () {
                var $todoItem = $(this);
                Todos.add({
                    'TodoId': $todoItem.data('todoid'),
                    'IsComplete': $todoItem.data('iscomplete'),
                    'Location': $todoItem.data('location'),
                    'Text': $todoItem.find('.todo').html()
                });
            });

            // Subscribe to changes in todo
            Todos.on('add', this.renderOne, this);
            Todos.on('reset', this.render, this);
        },
        render: function () {
            // Render all the todos
            this.$el.find('#todosList').html("");
            Todos.each(this.renderOne, this);
        },
        renderOne: function (todo) {
            // Render an individual todo to the end of our list
            this.$el.find('#todosList').append(this.template(todo.toJSON()));
        }
    });
    var todosView = new TodosListView();
})();