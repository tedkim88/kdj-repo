import { Text, View, StyleSheet, TouchableOpacity } from "react-native";
import useTheme from "@/hooks/useTheme";
import { useQuery } from "convex/react";
import { api } from "@/convex/_generated/api";
// import { useMutation } from "convex/react";
export default function Index() {
  const { toggleDarkMode } = useTheme();
  //just like zustand. instead of getting this from store, I get this from useTheme

  const todos = useQuery(api.todos.getTodos);
  console.log(todos);

  // const addTodo = useMutation(api.todos.addTodo);
  // const clearTodos = useMutation(api.todos.clearAllTodos);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Content testing</Text>
      {/* <Link href="/about">Visit About Screen</Link> */}
      <TouchableOpacity onPress={toggleDarkMode}>
        <Text>Toggle Theme</Text>
      </TouchableOpacity>
      {/* <TouchableOpacity onPress={() => addTodo({ text: "walk the dog!!" })}>
        <Text>Add a new Todo</Text>
      </TouchableOpacity>
      <TouchableOpacity onPress={() => clearTodos()}>
        <Text>Clear All Todos</Text>
      </TouchableOpacity> */}
    </View>
  );
}

//react native is file-routing system unlike next.js

const styles = StyleSheet.create({
  container: {
    flex: 1, //min-height: 100vh 같은느낌인듯.. 세로공간 100%차지
    alignItems: "center",
    justifyContent: "center",
  },
  title: {
    fontSize: 20,
    fontWeight: "bold",
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: "80%",
  },
});

