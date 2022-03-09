using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameMainTests
{

    GameMain game;
    InputMain input;

    [SetUp]  // The SetUp attribute specifies that this method is called before each test is run.
    public void Setup()
    {
        GameObject go = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameMain"));
        game = go.GetComponent<GameMain>();
        go = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Rewired Input manager"));
        go = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/InputMain"));
        input = go.GetComponent<InputMain>();
    }

    [Test]
    public void TestInputMain()
    {
        Assert.IsNotNull(input.listeners[0]);
        Assert.IsNotNull(input.listeners[1]);
        Assert.IsNotNull(input.listeners[2]);

        Assert.IsNull(input.listeners[0].jump);
    }


    [Test]
    public void NewTestScriptSimplePasses()
    {
        Assert.IsNotNull(game.someBool);
        Assert.IsNull(game.otherBool);
    }

    [Test]
    public void TestGameMainDoSomething()
    {
        Assert.IsTrue(game.DoSomething(false));
    }

    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        yield return null;
    }

    [TearDown] // The TearDown attribute specifies that this method is called after each test is run.
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }
}
