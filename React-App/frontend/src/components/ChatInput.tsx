import type { User } from "../lib/types";

type Props = {
  selectedUser: User | null; 
};

export default function ChatInput({ selectedUser }: Props) {
  return <div>{selectedUser ? selectedUser.name : "No user selected"}</div>;
}
