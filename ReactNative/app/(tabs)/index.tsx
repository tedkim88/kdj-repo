import {
  Text,
  View,
  StyleSheet,
  TouchableOpacity,
  StatusBar,
  FlatList,
  Alert,
  TextInput,
} from "react-native";
import useTheme from "@/hooks/useTheme";
import { useMutation, useQuery } from "convex/react";
import { api } from "@/convex/_generated/api";
import { ColorScheme } from "@/hooks/useTheme"; // ColorScheme
import { createHomeStyles } from "@/assets/styles/home.styles";
import { SafeAreaView } from "react-native-safe-area-context";
import { LinearGradient } from "expo-linear-gradient";
import Header from "@/components/Header";
import TodoInput from "@/components/TodoInput";
import LoadingSpinner from "@/components/LoadingSpinner";
import { Doc, Id } from "@/convex/_generated/dataModel";
import { Ionicons } from "@expo/vector-icons";
import { deleteTodo, toggleTodo } from "@/convex/todos";
import EmptyState from "@/components/EmptyState";
import { useState } from "react";

type Todo = Doc<"todos">;

export default function Index() {
  const { toggleDarkMode, colors } = useTheme();
  //just like zustand. instead of getting this from store, I get this from useTheme

  const [editingId, setEditingId] = useState<Id<"todos"> | null>(null);
  const [editText, setEditText] = useState("");

  const todos = useQuery(api.todos.getTodos);
  const toggleTodo = useMutation(api.todos.toggleTodo);
  const deleteTodo = useMutation(api.todos.deleteTodo);
  const updateTodo = useMutation(api.todos.updateTodo);

  console.log(todos);
  const homeStyles = createHomeStyles(colors);

  const isLoading = todos === undefined;

  const handleToggleTodo = async (id: Id<"todos">) => {
    try {
      await toggleTodo({ id });
    } catch (error) {
      console.log("Error toggling todo:", error);
      Alert.alert("Error", "Failed to toggle todo");
    }
  };

  const handleDeleteTodo = async (id: Id<"todos">) => {
    try {
      Alert.alert("Delete Todo", "Are you sure you want to delete this todo", [
        {
          text: "Cancel",
          style: "cancel",
        },
        {
          text: "Delete",
          style: "destructive",
          onPress: () => deleteTodo({ id }),
        },
      ]);
    } catch (error) {
      console.log("Error deleting todo:", error);
      Alert.alert("Error", "Failed to delete todo");
    }
  };

  const handleEditTodo = async (todo: Todo) => {
    setEditText(todo.text);
    setEditingId(todo._id);
  };

  const handleSaveEdit = async () => {
    if (editingId) {
      try {
        await updateTodo({ id: editingId, text: editText.trim() });
        setEditText("");
        setEditingId(null);
      } catch (error) {
        console.log("Error updating todo:", error);
        Alert.alert("Error", "Failed to update todo");
      }
    }
  };

  const handleCancelEdit = () => {
    setEditText("");
    setEditingId(null);
  };

  if (isLoading) return <LoadingSpinner />;

  const renderTodoItem = ({ item }: { item: Todo }) => {
    const isEditing = editingId === item._id;

    return (
      <View style={homeStyles.todoItemWrapper}>
        <LinearGradient
          colors={colors.gradients.surface}
          style={homeStyles.todoItem}
          start={{ x: 0, y: 0 }}
          end={{ x: 1, y: 1 }}
        >
          <TouchableOpacity
            style={homeStyles.checkbox}
            activeOpacity={0.7}
            onPress={() => handleToggleTodo(item._id)}
          >
            <LinearGradient
              colors={
                item.isCompleted
                  ? colors.gradients.success
                  : colors.gradients.muted
              }
              style={[
                homeStyles.checkboxInner,
                {
                  borderColor: item.isCompleted ? "transparent" : colors.border,
                },
              ]}
            >
              {item.isCompleted && (
                <Ionicons name="checkmark" size={18} color="#fff" />
              )}
            </LinearGradient>
          </TouchableOpacity>

          {isEditing ? (
            <View style={homeStyles.editContainer}>
              <TextInput
                style={homeStyles.editInput}
                value={editText}
                onChangeText={setEditText}
                autoFocus
                multiline
                placeholder="Edit your todo..."
                placeholderTextColor={colors.textMuted}
              />
              <View style={homeStyles.editButtons}>
                <TouchableOpacity onPress={handleSaveEdit} activeOpacity={0.8}>
                  <LinearGradient
                    colors={colors.gradients.success}
                    style={homeStyles.editButton}
                  >
                    <Ionicons name="checkmark" size={16} color="#fff" />
                    <Text style={homeStyles.editButtonText}>Save</Text>
                  </LinearGradient>
                </TouchableOpacity>

                <TouchableOpacity
                  onPress={handleCancelEdit}
                  activeOpacity={0.8}
                >
                  <LinearGradient
                    colors={colors.gradients.muted}
                    style={homeStyles.editButton}
                  >
                    <Ionicons name="close" size={16} color="#fff" />
                    <Text style={homeStyles.editButtonText}>Cancel</Text>
                  </LinearGradient>
                </TouchableOpacity>
              </View>
            </View>
          ) : (
            <View style={homeStyles.todoTextContainer}>
              <Text
                style={[
                  homeStyles.todoText,
                  item.isCompleted && {
                    textDecorationLine: "line-through",
                    color: colors.textMuted,
                    opacity: 0.6,
                  },
                ]}
              >
                {item.text}
              </Text>

              <View style={homeStyles.todoActions}>
                <TouchableOpacity
                  onPress={() => handleEditTodo(item)}
                  activeOpacity={0.8}
                >
                  <LinearGradient
                    colors={colors.gradients.warning}
                    style={homeStyles.actionButton}
                  >
                    <Ionicons name="pencil" size={14} color="#fff" />
                  </LinearGradient>
                </TouchableOpacity>
                <TouchableOpacity
                  onPress={() => {
                    handleDeleteTodo(item._id);
                  }}
                  activeOpacity={0.8}
                >
                  <LinearGradient
                    colors={colors.gradients.danger}
                    style={homeStyles.actionButton}
                  >
                    <Ionicons name="trash" size={14} color="#fff" />
                  </LinearGradient>
                </TouchableOpacity>
              </View>
            </View>
          )}
        </LinearGradient>
      </View>
    );
  };

  // const addTodo = useMutation(api.todos.addTodo);
  // const clearTodos = useMutation(api.todos.clearAllTodos);

  return (
    // <View style={homeStyles.container}>
    <LinearGradient
      colors={colors.gradients.background}
      style={homeStyles.container}
    >
      {/* 그냥View하고 차이..스태터스바 안넘어가도록하는기능인듯 */}
      <StatusBar barStyle={colors.statusBarStyle} />
      <SafeAreaView style={homeStyles.safeArea}>
        {/* 맨위에 시계보이는 가장윗라인 스타일링 */}
        <Header />

        <TodoInput />

        {/* <Text style={homeStyles.title}>Content testing</Text> */}
        {/* <Link href="/about">Visit About Screen</Link> */}

        <FlatList
          data={todos}
          renderItem={renderTodoItem}
          keyExtractor={(item) => item._id}
          style={homeStyles.todoList}
          contentContainerStyle={homeStyles.todoListContent}
          ListEmptyComponent={<EmptyState />}

          // showsVerticalScrollIndicator={false}
        />

        {/* 전부렌더링하는게아니라..UI에 보이는것만 성능 효율적으로 렌더링할 때는 FlatList(react-native) */}
        {/* {todos?.map((todo) => (
          <Text key={todo._id}>{todo.text}</Text>
        ))} */}

        {/* <TouchableOpacity onPress={toggleDarkMode}>
          <Text>Toggle Theme</Text>
        </TouchableOpacity> */}
        {/* <TouchableOpacity onPress={() => addTodo({ text: "walk the dog!!" })}>
        <Text>Add a new Todo</Text>
        </TouchableOpacity>
        <TouchableOpacity onPress={() => clearTodos()}>
        <Text>Clear All Todos</Text>
        </TouchableOpacity> */}
      </SafeAreaView>
    </LinearGradient>
  );
}

//react native is file-routing system unlike next.js

// const createStyles = (colors:ColorScheme) => {
//   const styles = StyleSheet.create({
//     container: {
//       flex: 1, //min-height: 100vh 같은느낌인듯.. 세로공간 100%차지
//       alignItems: "center",
//       justifyContent: "center",
//       gap: 10,
//       backgroundColor: colors.bg
//     },
//     title: {
//       fontSize: 20,
//       fontWeight: "bold",
//     },
//     separator: {
//       marginVertical: 30,
//       height: 1,
//       width: "80%",
//     },
//   });

//   return styles;
// };
