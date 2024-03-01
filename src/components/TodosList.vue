<template>
  <div class="todos-container">
   
    <div v-for="todo in todos" :key="todo.id" class="todo-item">
      <input type="checkbox" v-model="todo.completed" class="checkbox" />
      <div class="todo-details">
        <div class="title" :class="{ 'completed': todo.completed }">{{ todo.title }}</div>
        <div class="date" :class="{ 'completed': todo.completed }">{{ formatDate(todo.date) }}</div>
      </div>
      <div class="button-group">
        <button @click="editTodo(todo)">Update</button>
        <button @click="deleteTodo(todo.id)">Delete</button>
      </div>
    </div>
    <div class="add-todo-form">
      <label for="newTodo">Title:</label>
      <input type="text" v-model="newTodo.title" id="newTodo" placeholder="New todo"/>
<br><br>
      <label for="newDate">Date:</label>
      <input type="text" v-model="newTodo.date" id="newDate" ref="datePicker" placeholder=" yyyy-mm-dd"/>
<br><br>
      <button @click="saveTodo">{{ editingTodo ? 'Update' : 'Save' }}</button>
    </div>
  </div>
</template>

<script>
import flatpickr from 'flatpickr';
import 'flatpickr/dist/flatpickr.min.css';

export default {
  name: 'todosList',

  data() {
    return {
      todos: [],
      newTodo: { title: '', date: '' },
      editingTodo: null,
    };
  },

  created() {
    this.fetchTodos();
  },

  mounted() {
    this.setupDatePicker();
  },

  methods: {
    formatDate(dateString) {
      const date = new Date(dateString);
      const day = date.getDate().toString().padStart(2, '0');
      const month = (date.getMonth() + 1).toString().padStart(2, '0');
      const year = date.getFullYear();
      return `${year}-${month}-${day}`;
    },

    fetchTodos() {
      fetch('http://localhost:34457/api/Todos')
        .then(data => data.json())
        .then(todos => {
          this.todos = todos.map(todo => ({ ...todo, completed: false }));
        });
    },

    editTodo(todo) {
      this.editingTodo = todo;
      this.newTodo.title = todo.title;
      this.newTodo.date = this.formatDate(todo.date);
    },

    deleteTodo(todoId) {
      fetch(`http://localhost:34457/api/Todos/${todoId}`, {
        method: 'DELETE',
      })
        .then(() => {
          this.fetchTodos();
        });
    },

    saveTodo() {
      if (this.editingTodo) {
        fetch(`http://localhost:34457/api/Todos/${this.editingTodo.id}`, {
          method: 'PUT',
          body: JSON.stringify({
            title: this.newTodo.title,
            date: this.newTodo.date,
            completed: this.editingTodo.completed,
          }),
          headers: { 'Content-Type': 'application/json' },
        })
          .then(() => {
            this.editingTodo = null;
            this.fetchTodos();
          });
      } else {
        const newTodo = {
          title: this.newTodo.title,
          date: this.newTodo.date,
          completed: false,
        };

        fetch('http://localhost:34457/api/Todos', {
          method: 'POST',
          body: JSON.stringify(newTodo),
          headers: { 'Content-Type': 'application/json' },
        })
          .then(() => {
            this.fetchTodos();
            this.newTodo = { title: '', date: '' };
          });
      }
    },

    setupDatePicker() {
      flatpickr(this.$refs.datePicker, {
        dateFormat: 'Y-m-d',
      });
    },
  },
};
</script>

<style scoped>
.todos-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.todo-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 25px;
}

.todo-details {
  flex: 1;
}

.button-group {
  display: flex;
  gap: 10px;
  margin-left: 20px;
}

.add-todo-form {
  margin-top: 50px;
}

.completed {
  text-decoration: line-through;
  color: red;
}
</style>
