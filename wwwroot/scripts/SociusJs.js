const Socius = (() => {


	const createPost = (profileId, feedType, section, smPost) => {
		const post = document.createElement("article");
		post.classList.add("post");

		const postLink = document.createElement("a");
		postLink.onclick = addInteraction(profileId, feedType);
		postLink.href = smPost.postLink;
		postLink.rel = "nofollow";
		postLink.target = "_blank";

		if (smPost.attachment.mediaUrl) {
			if (smPost.attachment.mediaType == "photo") {
				const postMedia = document.createElement("img");
				postMedia.src = smPost.attachment.mediaUrl;
				postLink.appendChild(postMedia);
			}
			else if (smPost.attachment.mediaType == "video") {
				const postMedia = document.createElement("video");
				postMedia.src = smPost.attachment.mediaUrl;
				postLink.appendChild(postMedia);
			}
		}

		const postText = document.createElement("div");

		const postMessage = document.createElement("p");
		postMessage.innerText = smPost.message;
		postText.appendChild(postMessage);

		const postDate = document.createElement("small");
		postDate.innerText = smPost.createdAt;
		postText.appendChild(postDate);

		postLink.appendChild(postText);
		post.appendChild(postLink);
		section.appendChild(post);
	}


    const getFeedsAsync = async (profileId) => {
		const response = await fetch(`/socius/feeds/${profileId}`);
		const feeds = await response.json();

		return feeds;
	}

	const renderFeedsAsync = async (profileId, parentSelector, showTitle = true) => {
		const feeds = await getFeedsAsync(profileId);
		if (feeds == null) {
			return;
		}

		const parent = document.getElementById(parentSelector);
		if (parent == null) {
			return;
		}

		if (feeds.facebookPosts.length > 0) {

			const fbSection = document.createElement("section");
			fbSection.classList.add("Facebook");
			if (showTitle) {
				const fbTitle = document.createElement("h3");
				fbTitle.innerText = "Facebook";
				fbSection.appendChild(fbTitle);
			}
			const fbFeed = document.createElement("div");
			fbFeed.classList.add("feed");
			fbSection.appendChild(fbFeed);
			feeds.facebookPosts.forEach(fbPost => {
				createPost(profileId, 1, fbFeed, fbPost);
			});
			parent.appendChild(fbSection);
		}

		if (feeds.instagramPosts.length > 0) {

			const igSection = document.createElement("section");
			igSection.classList.add("Instagram");
			if (showTitle) {
				const igTitle = document.createElement("h3");
				igTitle.innerText = "Instagram";
				igSection.appendChild(igTitle);
			}
			const igFeed = document.createElement("div");
			igFeed.classList.add("feed");
			igSection.appendChild(igFeed);
			feeds.instagramPosts.forEach(igPost => {
				createPost(profileId, 2, igFeed, igPost);
			});
			parent.appendChild(igSection);
		}

		if (feeds.twitterPosts.length > 0) {

			const twSection = document.createElement("section");
			twSection.classList.add("Twitter");
			if (showTitle) {
				const twTitle = document.createElement("h3");
				twTitle.innerText = "Twitter";
				twSection.appendChild(twTitle);
			}
			const twFeed = document.createElement("div");
			twFeed.classList.add("feed");
			twSection.appendChild(twFeed);
			feeds.twitterPosts.forEach(twPost => {
				createPost(profileId, 3, twFeed, twPost);
			});
			parent.appendChild(twSection);
		}

	}


	const addInteraction = async (profileId, feedType) => {
		const interaction = await fetch('/socius/interaction', {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({
				profileId: profileId,
				feedType: feedType
			})
		});
	}


	return {
		getFeedsAsync: getFeedsAsync,
		renderFeedsAsync: renderFeedsAsync,
		addInteraction: addInteraction
	}

})();