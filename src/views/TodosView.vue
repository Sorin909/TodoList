<template>
    <div>
    
      <TodosList :key="todos.length" />
    </div>
  </template>
  
  <script>
  import TodosList from '@/components/TodosList.vue';
  
  export default {
    components: {
      TodosList
    },
  
    data() {
      return {
        todos: [],
        title: '',
        date: ''
      };
    },
  
    created() {
      this.fetchTodos();
    },
  
    methods: {
      fetchTodos() {
        fetch('http://localhost:34457/api/Todos')
          .then(data => data.json())
          .then(todos => {
            this.todos = todos;
          });
      }
    }
  };
  </script>
  
  <style scoped>
 
  </style>
  