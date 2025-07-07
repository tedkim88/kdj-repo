import type { User } from "../lib/types";

type Props = {
  selectedUser: User | null;
};

export default function ChatWindow({ selectedUser }: Props) {
  return (
    <div className="flex-1 bg-gray-900 text-gray-100 p-6 overflow-y-auto">
      {selectedUser ? (
        <div className="text-xl text-yellow-300 font-bold">
          <span>To:</span>{" "}
          <span className="uppercase">{selectedUser.name}</span>
        </div>
      ) : (
        <div className="text-gray-500 italic">No user selected</div>
      )}
    </div>
  );
}
