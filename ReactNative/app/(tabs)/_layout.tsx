import { Tabs } from "expo-router/tabs";
import { Ionicons } from "@expo/vector-icons";
import React from "react";
import useTheme from "@/hooks/useTheme";
export default function TabsLayout() {
  
const {colors} = useTheme();


  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: colors.primary,
        tabBarInactiveTintColor: colors.textMuted,
        tabBarStyle: {
          backgroundColor: colors.surface,
          borderTopWidth: 1,
          borderTopColor: colors.border,
          height: 90,
          paddingBottom: 30,
          paddingTop: 10,
        },
        headerShown: false, //지금내가들어와있는 메뉴..
      }}
    >
      <Tabs.Screen
        name="index" //이게실제파일명 .tsx
        options={{
          title: "Todos", //이게 폰 화면에나오는 이름
          tabBarIcon: ({ color, size }) => (
            <Ionicons name="flash-outline" color={color} size={size} /> //name속성으로 아이콘모양바뀜.
          ),
        }}
      />

      <Tabs.Screen
        name="settings"
        options={{
          title: "Settings",
          tabBarIcon: ({ color, size }) => (
            <Ionicons name="settings" color={color} size={size} />
          ),
        }}
      />
    </Tabs>
  );
}
