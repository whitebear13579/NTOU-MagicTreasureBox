import axios from "axios";
import Post from "./components/Posts";

async function getData(){
    const res = await axios.get("https://catfact.ninja/fact");
    return res.data.fact as string;
}

export default async function PostContainer() {
  const facts = await Promise.all(Array.from({length:10},()=>getData()));
  
  return (
    <div className="flex flex-col gap-4 m-4" >
      {facts.map((fact, index) => (
        <Post key={index} description={fact} />
      ))}
    </div>
  );
}
